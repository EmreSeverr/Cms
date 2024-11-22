namespace Cms.Entity
{
    public class Category : BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual IEnumerable<CategoryLanguage> Languages { get; set; }

        public virtual IEnumerable<Content> Contents { get; set; }
    }
}
