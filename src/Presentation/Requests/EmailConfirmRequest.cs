namespace CleanMinimalApi.Presentation.Requests;

public class EmailConfirmRequest
{
    public string UserId { get; set; }
    public string Token { get; set; }
}
