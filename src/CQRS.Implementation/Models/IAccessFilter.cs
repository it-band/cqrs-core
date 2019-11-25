using CQRS.Models;

namespace CQRS.Implementation.Models
{
    public interface IAccessFilter<T> : IQueryableFilter<T>
        where T : class
    {
    }
}
