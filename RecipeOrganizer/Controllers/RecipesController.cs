namespace RecipeManager;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [HttpGet]
    public IActionResult GetAllRecipes() => Ok( _recipeService.GetAllRecipes());

    //use it when insert recipe and add the user in hereeeeeeeeeeeeeeeeee
    //public IActionResult abc()
    //{
    //    return Ok(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
    //}

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRecipeWithIngredients(Guid id) => Ok(_recipeService.GetRecipeWithIngredient(id));

    [HttpPost("getRecipeByIngredient")]
    public IActionResult GetRecipeByListOfIngredients([FromBody]List<string> ingredients) => Ok(_recipeService.GetRecipeByListOfIngredients(ingredients));

    [HttpPost("add")]
    public IActionResult AddRecipe(Recipe recipe)
    {
        var r = _recipeService.AddRecipe(recipe);
        var user = User.FindFirstValue;

        if (user is not null)
        {
            return Ok(r + "\n" + user);
        }

        return Ok(_recipeService.AddRecipe(recipe));
    }

    [HttpPut("edit")]
    public IActionResult UpdateRecipe(Recipe recipe, Guid id) => Ok( _recipeService.UpdateRecipe(recipe , id));

    [HttpDelete]
    public IActionResult RemoveRecipe(Guid id) => Ok( _recipeService.RemoveRecipe(id));
}