namespace Cms.Api.DTO
{
    public class AddContentVariantDto
    {
        public int ContentId { get; set; }
        public IEnumerable<IEnumerable<ContentVariantDto>> ContentVariants { get; set; }
    }
}
