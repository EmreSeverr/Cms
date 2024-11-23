namespace Cms.Entity
{
    public class Content : BaseEntity
    {
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<ContentLanguage> Languages { get; set; }
        public virtual ICollection<UserContentVariantHistory> VariantHistories { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}