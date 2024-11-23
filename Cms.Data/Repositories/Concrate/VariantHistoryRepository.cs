using Cms.Data.Repositories.Abstract;
using Cms.Entity;

namespace Cms.Data.Repositories.Concrate
{
    public class VariantHistoryRepository : CmsBaseRepository<UserContentVariantHistory>, IVariantHistoryRepository
    {
        public VariantHistoryRepository(CmsDbContext cmsDbContext) : base(cmsDbContext)
        {

        }
    }
}
