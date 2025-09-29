using Common.Query;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistent.EF.Context;
using Shop.Query.Users.DTOs;

namespace Shop.Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler : IQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly BookStoreContext _context;

    public GetUserByPhoneNumberQueryHandler(BookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(f => f.PhoneNumber == request.PhoneNumber, cancellationToken);

        if (user == null)
            return null;


        return await user.Map().SetUserRoleTitles(_context);
    }
}