namespace MicroTest1.Messages
{
    using MicroTest1.Messages;
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        string Code { get; }
    }
}