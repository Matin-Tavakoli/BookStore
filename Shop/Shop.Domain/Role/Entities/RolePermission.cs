using Common.Domain;
using Shop.Domain.Role.Enums;

namespace Shop.Domain.Role.Entities
{
    public class RolePermission:BaseEntity
    {
        public RolePermission(Permission permission)
        {
            Permission = permission;
        }

        public long RoleId { get; internal set; }
        public Permission Permission { get; private set; }
    }
}
