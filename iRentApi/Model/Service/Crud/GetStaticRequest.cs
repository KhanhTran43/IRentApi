namespace iRentApi.Model.Service.Crud
{
    public class GetStaticRequest
    {
        public List<string> Includes { get; set; } = new List<string>();
        public Dictionary<string, Action<object>> Resolves { get;} = new Dictionary<string, Action<object>>();
    }
}
