﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecipeOrganizer;

[Route("api/[controller]")]
[ApiController]
public class AuthunticationController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthunticationController(IEmailService emailService,
                                    IConfiguration configuration ,
                                    UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager, 
                                    SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _emailService = emailService;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser, string role)
    {
        //Check User Exist 
        var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
        if (userExist != null)
        {
            return StatusCode(StatusCodes.Status403Forbidden,
                new Response { Status = "Error", Message = "User already exists!" });
        }

        //Add the User in the database
        IdentityUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.Username,
            TwoFactorEnabled = true
        };

        if (await _roleManager.RoleExistsAsync(role))
        {
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User Failed to Create" });
            }
            //Add role to the user....

            await _userManager.AddToRoleAsync(user, role);

            //Add Token to Verify the email....

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });

        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "This Role Does not Exist." });
        }

    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK,
                  new Response { Status = "Success", Message = "Email Verified Successfully" });
            }
        }
        return StatusCode(StatusCodes.Status500InternalServerError,
                   new Response { Status = "Error", Message = "This User Does not exist!" });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] Login loginModel)
    {
        var user = await _userManager.FindByNameAsync(loginModel.Username);
        if (user.TwoFactorEnabled)
        {
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, true);
            var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            var message = new Message(new string[] { user.Email! }, "OTP Confrimation", token);
            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
             new Response { Status = "Success", Message = $"We have sent an OTP to your Email {user.Email}" });
        }
        if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }


            var jwtToken = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            });

            //returning the token...

        }
        return Unauthorized();


    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(2),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }

}