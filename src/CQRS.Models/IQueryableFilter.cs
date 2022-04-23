using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Models
{
    public interface IQueryableFilter<T>
        where T : class
    {
        Task<IQueryable<T>> Apply(IQueryable<T> query, DbContext dbContext = null);
    }
}
