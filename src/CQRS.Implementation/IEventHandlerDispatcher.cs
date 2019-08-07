using System.Threading.Tasks;
using CQRS.Abstractions.Models;

namespace CQRS.Implementation
{
    public interface IEventHandlerDispatcher
    {
        Task Handle<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
