using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace BCPAO.PhotoManager.Data
{
    public class Role : IdentityRole<int>
    {
        public Role() : base()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
