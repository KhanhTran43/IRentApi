using Domain.Model;
using iRentApi.Model.Entity.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entity
{
    public class Role : EntityBase<long>
    {
        public string Name { get; set; }
    }
}
