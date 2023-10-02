namespace RO.Services;

public interface IFeedBackService
{
    IEnumerable<FeedBack> GetAllFeedbacks();

    FeedBack GetFeedbackById(Guid id);

    string AddFeedback(FeedBack Feedback);

    string UpdateFeedback(FeedBack Feedback, Guid id);

    string RemoveFeedback(Guid id);
}