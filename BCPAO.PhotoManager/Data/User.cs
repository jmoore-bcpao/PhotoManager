using BCPAO.PhotoManager.Models.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace BCPAO.PhotoManager.Data
{
    public class User : IdentityUser<int>
    {
        public User() : base()
        {
            UserPermissions = new HashSet<UserPermission>();
        }

        public UserType UserType { get; set; }
        
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
