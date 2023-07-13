using Domain.Model;
using Domain.Model.Entity.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entity
{
    public class UserRole
    {
        public UserRoleKeys UserRoleKeys { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
