using Cms.Data.Repositories.Abstract;
using Cms.Entity;

namespace Cms.Data.Repositories.Concrate
{
    public class UserRepository : CmsBaseRepository<User>, IUserRepository
    {
        public UserRepository(CmsDbContext dbContext) : base(dbContext)
        {

        }
    }
}
