using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entity
{
    public class User
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        long Id { get; set; }
        string Name { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        [EmailAddress]
        string Email { get; set; }
        string Address { get; set; }
        DateTime DateOfBirth { get; set; }
        [Phone]
        string PhoneNumber { get; set; }
    }
}
