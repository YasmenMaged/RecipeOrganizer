namespace RO.Services;

public class FeedBackService : IFeedBackService
{
    private IRepository<FeedBack> _repository { get; set; }
    public FeedBackService(IRepository<FeedBack> repository)
    {
        _repository = repository;
    }

    public IEnumerable<FeedBack> GetAllFeedbacks() => _repository.GetAll();

    public FeedBack GetFeedbackById (Guid id) => _repository.Get(id);

    public string AddFeedback(FeedBack Feedback) => _repository.Insert(Feedback);

    public string UpdateFeedback(FeedBack Feedback, Guid id)
    {
        id = Feedback.Id;

        return _repository.Update(Feedback, id);
    }

    public string RemoveFeedback(Guid id)
    {
        FeedBack Feedback = _repository.Get(id);
        _repository.Remove(Feedback);
        _repository.SaveChanges();

        return "Removed!!";
    }
}