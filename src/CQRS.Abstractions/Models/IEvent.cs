namespace CQRS.Abstractions.Models
{
    public interface IEvent
    {
        string QueueName { get; set; }
    }
}
