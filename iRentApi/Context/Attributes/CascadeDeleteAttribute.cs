using Microsoft.EntityFrameworkCore;

namespace iRentApi.Context.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class CascadeDeleteAttribute : Attribute
    {
        public DeleteBehavior DeleteBehavior { get; }

        public CascadeDeleteAttribute(DeleteBehavior deleteBehavior)
        {
            DeleteBehavior = deleteBehavior;
        }
    }
}
