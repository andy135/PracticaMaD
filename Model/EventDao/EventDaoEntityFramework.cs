using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
	class EventDaoEntityFramework :
        GenericDaoEntityFramework<Event, Int64>, IEventDao
    {
        public List<EventInfo> FindEvents(String[] keywords, long? categoryId, int startIndex, int count)
        {
			
			DbSet<Event> events = Context.Set<Event>();
			List<Event> result;
            if (categoryId == null && keywords!=null)
			{
				result =
					(from e in events
					 where keywords.All(s => e.eventName.Contains(s)) && e.date>DateTime.Now
					 orderby e.date
					 select e).Skip(startIndex).Take(count).ToList();
			}
			else
			{
				if (keywords != null)
				{
					result =
						(from e in events
						 where keywords.All(s => e.eventName.Contains(s)) && e.categoryId == categoryId && e.date > DateTime.Now
						 orderby e.date
						 select e).Skip(startIndex).Take(count).ToList();
				}
				else
				{
					result =
						(from e in events
						 where e.categoryId == categoryId && e.date > DateTime.Now
						 orderby e.date
						 select e).Skip(startIndex).Take(count).ToList();
				}
			}

			List<EventInfo> eventsinfo = new List<EventInfo>();
			EventInfo ei;
			foreach (Event e in result)
			{
				ei = new EventInfo(e.eventId, e.eventName, e.review, e.date, e.categoryId, e.Category.categoryName,e.Comment.Count);
				eventsinfo.Add(ei);
			}

			return eventsinfo;
        }

		public int GetNumberOfEvents(String[] keywords, long? categoryId)
		{

			DbSet<Event> events = Context.Set<Event>();
			int result;
			if (categoryId == null && keywords != null)
			{
				result =
					(from e in events
					 where keywords.All(s => e.eventName.Contains(s)) && e.date > DateTime.Now
					 orderby e.date
					 select e).Count();
			}
			else
			{
				if (keywords != null)
				{
					result =
						(from e in events
						 where keywords.All(s => e.eventName.Contains(s)) && e.categoryId == categoryId && e.date > DateTime.Now
						 orderby e.date
						 select e).Count();
				}
				else
				{
					result =
						(from e in events
						 where e.categoryId == categoryId && e.date > DateTime.Now
						 orderby e.date
						 select e).Count();
				}
			}
			return result;
		}
	}
}
