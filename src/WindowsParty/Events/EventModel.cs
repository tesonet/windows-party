
namespace WindowsParty.Events
{
    public class EventModel
    {
        public EventModel(Status status)
        {
            Status = status;
        }

        public Status Status
        {
            get; set;
        }
    }
}
