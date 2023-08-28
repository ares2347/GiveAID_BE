namespace GAID.Api.Dto.User.Request;

public class RegisterRequest
{
    public string? Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? Password { get; set; }
    public string FullName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public List<string> Roles { get; set; }
}