using System.Linq;
using System.Threading.Tasks;

namespace CQRS.Models
{
    public interface IQueryableFilter<T>
        where T : class
    {
        Task<IQueryable<T>> Apply(IQueryable<T> query);
    }
}
