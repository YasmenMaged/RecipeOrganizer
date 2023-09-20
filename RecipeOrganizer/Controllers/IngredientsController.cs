namespace IngredientManager;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _IngredientService;

    public IngredientsController(IIngredientService IngredientService)
    {
        _IngredientService = IngredientService;
    }

    [HttpGet]
    public IActionResult GetAllIngredients() => Ok(_IngredientService.GetAllIngredients());

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetIngredientById(Guid id) => Ok(_IngredientService.GetIngredientById(id));

    [HttpPost("add")]
    public IActionResult AddIngredient(Ingredient Ingredient) => Ok(_IngredientService.AddIngredient(Ingredient));

    [HttpPut("edit")]
    public IActionResult UpdateIngredient(Ingredient Ingredient, Guid id) => Ok(_IngredientService.UpdateIngredient(Ingredient, id));

    [HttpDelete]
    public IActionResult RemoveIngredient(Guid id) => Ok(_IngredientService.RemoveIngredient(id));
}