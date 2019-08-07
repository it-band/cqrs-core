using System.Threading.Tasks;
using CQRS.Abstractions.Models;

namespace CQRS.Abstractions
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}
