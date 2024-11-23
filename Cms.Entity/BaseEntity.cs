using System.ComponentModel.DataAnnotations.Schema;

namespace Cms.Entity
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
