using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class Comment : EntityBase
    {
        public long PostId { get; set; }
        public Post Post { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
