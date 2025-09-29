using Common.Application;
using Shop.Domain.Role.Enums;

namespace Shop.Application.Roles.Edit;

public record EditRoleCommand(long Id, string Title, List<Permission> Permissions) : IBaseCommand;