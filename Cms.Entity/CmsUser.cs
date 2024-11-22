using Microsoft.AspNetCore.Identity;

namespace Cms.Entity
{
    public class CmsUser : IdentityUser<Guid>
    {
        public required string FullName { get; set; }

        public virtual IEnumerable<Content> Contens { get; set; }
    }
}
