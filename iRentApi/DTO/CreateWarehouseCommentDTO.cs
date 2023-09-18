namespace iRentApi.DTO
{
    public class CreateWarehouseCommentDTO
    {
        public long WarehouseId { get; set; }
        public long UserId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
