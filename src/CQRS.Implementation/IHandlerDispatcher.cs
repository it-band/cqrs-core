using System;
using System.Threading.Tasks;
using CQRS.Models;

namespace CQRS.Implementation
{
    public interface IHandlerDispatcher
    {
        Task<Result<TOut>> Handle<TIn, TOut>(TIn input);
        Task<Result<object>> Handle(Type In, object input);
    }
}
