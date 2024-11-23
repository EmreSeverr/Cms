namespace Cms.Entity
{
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }

        public virtual ICollection<UserContentVariantHistory> VariantHistories { get; set; }
        public virtual IEnumerable<Content> Contens { get; set; }
    }
}
