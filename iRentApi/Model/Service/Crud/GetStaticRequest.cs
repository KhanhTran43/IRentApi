namespace iRentApi.Model.Service.Crud
{
    public class GetStaticRequest
    {
        public List<string> Includes { get; set; } = new List<string>();
        public List<string> Resolves { get;} = new List<string>();
    }
}
