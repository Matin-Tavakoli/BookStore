using Shop.Domain.User.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.User
{
    internal class UserRepository : BaseRepository<Domain.User.Entites.User>, IUserRepository
    {
        public UserRepository(BookStoreContext context) : base(context)
        {
        }
    }
}