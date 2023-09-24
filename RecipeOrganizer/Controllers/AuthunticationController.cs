using Microsoft.AspNetCore.Identity;

namespace RecipeOrganizer;

[Route("api/[controller]")]
[ApiController]
public class AuthunticationController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    //private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthunticationController(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager, 
        SignInManager<IdentityUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        // _emailService = emailService;
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

            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
            //var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);
            //_emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK,
                new Response { Status = "Success", Message = $"User created & Email Sent to {user.Email} SuccessFully" });

        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "This Role Does not Exist." });
        }


        //Add role to the user....

    }
}