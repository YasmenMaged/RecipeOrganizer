using Microsoft.AspNetCore.Authorization;
namespace RecipeOrganizer;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    [HttpGet("users")]
    public IEnumerable<string> Get()
    {
        return new List<string> { "Ahmed", "Ali", "Ahsan" };
    }
}