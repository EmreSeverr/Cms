namespace Cms.Api.DTO
{
    public class AddContentDto
    {
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public IEnumerable<IEnumerable<ContentVariantDto>> ContentVariants { get; set; }
    }

    public class ContentVariantDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}
