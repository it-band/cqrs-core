using System.Linq;

namespace CQRS.Models
{
    public interface IAccessFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
