namespace Cms.Api.DTO
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public IEnumerable<ContentDto> Contens { get; set; }
    }
}
