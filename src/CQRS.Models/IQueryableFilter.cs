using System.Linq;

namespace CQRS.Models
{
    public interface IQueryableFilter<T>
        where T : class
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
