using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public class EventBlock
    {
        public List<Event> Events { get; private set; }
        public bool ExistMoreEvents { get; private set; }

        public EventBlock(List<Event> events, bool existMoreEvents)
        {
            this.Events = events;
            this.ExistMoreEvents = existMoreEvents;
        }
    }
}
