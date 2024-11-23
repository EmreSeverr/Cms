namespace Cms.Api.DTO
{
    public class CategoryLanguageDto
    {
        public string Title { get; set; }

        public int LanguageId { get; set; }
        public SystemLanguageDto Language { get; set; }

        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
