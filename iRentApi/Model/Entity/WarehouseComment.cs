using Domain.Model.Entity;
using iRentApi.Context.Attributes;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class WarehouseComment : EntityBase
    {
        public long WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public long UserId { get; set; }
        [CascadeDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.NoAction)]
        public User? User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string GetSenderName()
        {
            return User.Name;
        }
        public string GetSenderAvatar()
        {
            return "";
        }
        public ICollection<WarehouseCommentLike> CommentLikes { get; set; }
    }
}
