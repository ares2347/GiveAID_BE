namespace GAID.Api.Dto.User.Request;

public class CreateNewAccountRequest
{
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? FullName { get; set; }
}