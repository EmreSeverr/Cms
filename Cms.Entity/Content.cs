namespace Cms.Entity
{
    public class Content : BaseEntity
    {
        public string ImageUrl { get; set; }
        public Guid VariantId { get; set; } = Guid.NewGuid();

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int CmsUserId { get; set; }
        public virtual CmsUser CmsUser { get; set; }

        public virtual IEnumerable<ContentLanguage> Languages { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}