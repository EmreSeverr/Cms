namespace Cms.Api.DTO
{
    public class ContentLanguageDto : BaseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid VariantId { get; set; }

        public int ContentId { get; set; }
        public ContentDto Content { get; set; }

        public int LanguageId { get; set; }
        public SystemLanguageDto Language { get; set; }
    }
}
