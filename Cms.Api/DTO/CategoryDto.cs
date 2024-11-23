namespace Cms.Api.DTO
{
    public class CategoryDto : BaseDto
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public IEnumerable<CategoryLanguageDto> Languages { get; set; }

        public IEnumerable<ContentDto> Contents { get; set; }
    }
}
