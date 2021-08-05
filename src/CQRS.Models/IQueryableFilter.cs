using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Models
{
    public interface IQueryableFilter<T>
        where T : class
    {
        IQueryable<T> Apply(IQueryable<T> query);

        Task<IQueryable<T>> ApplyAsync(IQueryable<T> query);
    }
}
