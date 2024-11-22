namespace Cms.Entity
{
    public class CategoryLanguage : BaseEntity
    {
        public string Title { get; set; }

        public int LanguageId { get; set; }
        public virtual SystemLanguage Language { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
