using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;
using iRentApi.Model.Validation;
using System.ComponentModel.DataAnnotations;

namespace iRentApi.DTO
{
    public class UserDTO : ISelectDTO<User>, IInsertDTO<User>, IUpdateDTO<User>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        [UniqueEmail]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Ioc { get; set; }
        public DateTime Dob { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public Role Role { get; set; }
    }
}
