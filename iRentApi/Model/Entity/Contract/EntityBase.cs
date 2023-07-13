using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iRentApi.Model.Entity.Contract
{
    public class EntityBase<TKey>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
        public override bool Equals(object? obj)
        {
            return Equals(obj as EntityBase<TKey>, entity => Id.Equals(entity.Id));
        }

        protected bool Equals<T>(T obj, Predicate<T> equalsFunc) where T : EntityBase<TKey>
        {
            return obj is not null && GetType() == obj.GetType() && equalsFunc(obj);
        }
    }
}
