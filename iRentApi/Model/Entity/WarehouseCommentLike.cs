using Domain.Model.Entity;
using iRentApi.Context.Attributes;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class WarehouseCommentLike : EntityBase
    {
        public long CommentId { get; set; }
        public WarehouseComment? Comment { get; set; }
        public long UserId { get; set; }
        [CascadeDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.)]
        public User? User { get; set; }
        public string? Type { get; set; }
    }
}
