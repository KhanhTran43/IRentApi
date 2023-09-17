using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using iRentApi.Context.Attributes;

namespace iRentApi.Context.Convention
{
    public class CascadeDeleteConvention : IConvention
    {
        public void Apply(IMutableModel model)
        {
            System.Diagnostics.Debugger.Launch();
            foreach (var entityType in model.GetEntityTypes())
            {
                foreach (var navigation in entityType.GetNavigations())
                {
                    // Check if the navigation property has the CascadeDeleteAttribute
                    var cascadeDeleteAttribute = navigation.PropertyInfo.GetCustomAttributes(typeof(CascadeDeleteAttribute), false)
                        .FirstOrDefault() as CascadeDeleteAttribute;

                    if (cascadeDeleteAttribute != null)
                    {
                        navigation.ForeignKey.DeleteBehavior = cascadeDeleteAttribute.DeleteBehavior;
                    }
                }
            }
        }
    }
}
