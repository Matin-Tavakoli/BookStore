using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetList;

public class GetRoleListQueryHandler : IQueryHandler<GetRoleListQuery, List<RoleDto>>
{
    private readonly BookStoreContext _context;

    public GetRoleListQueryHandler(BookStoreContext context)
    {
        _context = context;
    }
    public async Task<List<RoleDto>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Roles.Select(role => new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Title = role.Title
        }).ToListAsync(cancellationToken);
    }
}