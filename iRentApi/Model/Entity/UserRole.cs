using Domain.Model;
using Domain.Model.Entity.Key;
using iRentApi.Model.Entity.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entity
{
    public class UserRole : EntityBase<UserRoleKeys>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
