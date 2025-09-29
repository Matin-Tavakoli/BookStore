using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Roles.DTOs;

namespace Shop.Query.Roles.GetById;

public class GetRoleByIdQueryHandler : IQueryHandler<GetRoleByIdQuery, RoleDto?>
{
    private readonly BookStoreContext _context;

    public GetRoleByIdQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<RoleDto?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == request.RoleId, cancellationToken: cancellationToken);
        if (role == null)
            return null;

        return new RoleDto()
        {
            Id = role.Id,
            CreationDate = role.CreationDate,
            Permissions = role.Permissions.Select(s => s.Permission).ToList(),
            Title = role.Title
        };
    }
}