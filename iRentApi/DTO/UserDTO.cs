using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using System.ComponentModel.DataAnnotations;

namespace iRentApi.DTO
{
    public class UserDTO : ISelectDTO<User>, IInsertDTO<User>, IUpdateDTO<User>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public ICollection<RoleDTO> Roles { get; set; }
    }
}
