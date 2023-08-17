using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class CommentDTO : ISelectDTO<Comment>, IInsertDTO<Comment>, IUpdateDTO<Comment>
    {
        public long Id { get; set; }
        public long PostId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

    }
}
