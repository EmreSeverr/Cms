namespace Cms.Entity
{
    public class ContentLanguage : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid VariantId { get; set; }


        public int ContentId { get; set; }
        public virtual Content Content { get; set; }

        public int LanguageId { get; set; }
        public virtual SystemLanguage Language { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
