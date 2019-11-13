using System.Threading.Tasks;
using CQRS.Abstractions.Models;

namespace CQRS.Abstractions
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}
