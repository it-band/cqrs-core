using CQRS.Abstractions.Models;

namespace CQRS.Abstractions
{
    public interface IQueryHandler<in TIn, out TOut> : IHandler<TIn, TOut>
        where TIn : IQuery<TOut>
    {
    }
}
