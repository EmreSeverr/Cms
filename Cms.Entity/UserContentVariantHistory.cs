namespace Cms.Entity
{
    public class UserContentVariantHistory : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Guid VariantId { get; set; }

        public int ContentId { get; set; }
        public virtual Content Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
