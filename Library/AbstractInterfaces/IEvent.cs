namespace Data.AbstractInterfaces
{
    public interface IEvent
    {
        int Id { get; set; }
        int StateId { get; set; }
        int UserId { get; set; }
        DateTime DateStamp { get; set; }
        string EventType { get; set; }
    }
}
