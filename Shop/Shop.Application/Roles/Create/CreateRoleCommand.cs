using Common.Application;
using Shop.Domain.Role;
using Shop.Domain.Role.Enums;

namespace Shop.Application.Roles.Create;

public record CreateRoleCommand(string Title, List<Permission> Permissions) : IBaseCommand;