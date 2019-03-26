using System.Linq;

namespace CQRS.Implementation.Models
{
    public interface IAccessFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
