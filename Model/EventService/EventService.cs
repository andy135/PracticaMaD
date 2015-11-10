using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public class EventService : IEventService
    {
        [Dependency]
        public IEventDao EventDao { private get; set; }

        public EventBlock FindEvents(List<string> keywords, long? categoryId, int startIndex, int count)
        {
            List<Event> events = EventDao.FindEvents(keywords, categoryId, startIndex, count + 1);

            bool existMoreEvents = (events.Count == count + 1);

            if (existMoreEvents)
                events.RemoveAt(count);

            return new EventBlock(events, existMoreEvents);
        }
    }
}
