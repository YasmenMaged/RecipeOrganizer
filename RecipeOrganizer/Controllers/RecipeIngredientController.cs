namespace RecipeOrganizer;

[Route("api/[controller]")]
[ApiController]
public class RecipeIngredientController : ControllerBase
{
    private readonly IRecipeIngredientRepository _recipeIngredientRepository;

    public RecipeIngredientController(IRecipeIngredientRepository recipeIngredientRepository)
    {
        _recipeIngredientRepository = recipeIngredientRepository;
    }

    [HttpGet]
    public IActionResult GetAllRecipeIngredients() => Ok(_recipeIngredientRepository.GetAll());

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetRecipeIngredientsById(Guid id) => Ok(_recipeIngredientRepository.Get(id));

    [HttpPost("add")]
    public IActionResult AddRecipeIngredient(RecipeIngredientRequest recipe_Ingredient) => Ok(_recipeIngredientRepository.Insert(recipe_Ingredient));

    [HttpPut("edit")]
    public IActionResult UpdateRecipeIngredients(Recipe_Ingredient recipe_Ingredient, Guid id) => Ok(_recipeIngredientRepository.Update(recipe_Ingredient, id));

    [HttpDelete]
    public IActionResult  DeleteRecipeIngredients(Guid id) => Ok(_recipeIngredientRepository.Delete(id));


}
