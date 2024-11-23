using Cms.Data.Repositories.Abstract;
using Cms.Entity;

namespace Cms.Data.Repositories.Concrate
{
    public class ContentRepository : CmsBaseRepository<Content>, IContentRepository
    {
        public ContentRepository(CmsDbContext cmsDbContext) : base(cmsDbContext)
        {

        }
    }
}
