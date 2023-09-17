using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class WarehouseComment : EntityBase
    {
        public long WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public long UserId { get; set; }
        public User? User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public ICollection<WarehouseCommentLike> CommentLikes { get; set; }
    }
}
