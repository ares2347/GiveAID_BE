namespace GAID.Api.Dto.User.Response
{
    public class UserInfoResponse
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Guid? ProfilePictureId { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
