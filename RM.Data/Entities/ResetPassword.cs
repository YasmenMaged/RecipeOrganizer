namespace RO.Data;

public class ResetPassword
{
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "The Password and Confirmation Password does not match .")]
    public string ConfirmPassword { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}