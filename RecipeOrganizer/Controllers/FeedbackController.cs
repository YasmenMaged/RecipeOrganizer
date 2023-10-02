namespace RecipeOrganizer;

[Route("api/[controller]")]
[ApiController]
public class FeedBackController : ControllerBase
{

    private readonly IFeedBackService _feedbackService;
    private readonly IMemoryCache _memoryCache;
    public FeedBackController(IFeedBackService feedbackService, IMemoryCache memoryCache)
    {
        _feedbackService = feedbackService;
        _memoryCache = memoryCache;
    }

    //Get All feedbacks
    [HttpGet]
    [Route("getAll")]
    public async Task<IActionResult> GetAllFeedbacks()
    {
        Console.WriteLine("here");
        var response = _feedbackService.GetAllFeedbacks();
        if (response.Count() == 0) return NotFound("No FeedBacks Found");
        return Ok(response);
    }

    [HttpGet]
    [Route("get/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = _feedbackService.GetFeedbackById(id);
        if (response != null) return Ok(response);
        return NotFound("No FeedBacks Found");

    }

    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add (FeedBack feedBack) => Ok(_feedbackService.AddFeedback(feedBack));


    //Remove feedback
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> RemoveFeedback([FromRoute] Guid id)
    {
        var response = _feedbackService.RemoveFeedback(id);
        if (response == "Not Found FeedBack.") return NotFound(response);
        return Ok(response);
    }
    //Update FeedBack
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateFeedback([FromRoute] FeedBack feedBack , Guid id)
    {
        var response = _feedbackService.UpdateFeedback(feedBack, id);

        if (response == "Not Found feedbacks.") return NotFound(response);
        return Ok(response);
    }
}