using Common.Application.Validation;
using Shop.Domain.Role.Repositories;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.EF.Context;

namespace Shop.Infrastructure.Persistent.EF.Role;

internal class RoleRepository : BaseRepository<Domain.Role.Entities.Role>, IRoleRepository
{
    public RoleRepository(BookStoreContext context) : base(context)
    {
    }
}