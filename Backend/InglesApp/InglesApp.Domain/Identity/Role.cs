using Microsoft.AspNetCore.Identity;

namespace InglesApp.Domain.Identity
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
