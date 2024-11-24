namespace Cms.Api.DTO
{
    public class AddContentImageDto
    {
        public int ContentId { get; set; }
        public IFormFile Image { get; set; }
    }
}
