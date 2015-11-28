using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
	public class EventBlock
    {
        public List<EventInfo> Events { get; private set; }
        public bool ExistMoreEvents { get; private set; }

        public EventBlock(List<EventInfo> events, bool existMoreEvents)
        {
            this.Events = events;
            this.ExistMoreEvents = existMoreEvents;
        }
    }
}
