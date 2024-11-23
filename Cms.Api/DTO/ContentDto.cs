namespace Cms.Api.DTO
{
    public class ContentDto : BaseDto
    {
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }

        public IEnumerable<ContentLanguageDto> Languages { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
