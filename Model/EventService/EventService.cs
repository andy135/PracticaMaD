﻿using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
	public class EventService : IEventService
    {
        [Dependency]
        public IEventDao EventDao { private get; set; }

        [Dependency]
        public ICategoryDao CategoryDao { private get; set; }

        public EventBlock FindEvents(String keys, long? categoryId, int startIndex, int count)
        {

			String[] keywords = null;
            if (keys != null && keys != "")
			{
				keywords = keys.Split(' ');
			}

            List<EventInfo> events = EventDao.FindEvents(keywords, categoryId, startIndex, count + 1);

            bool existMoreEvents = (events.Count == count + 1);

            if (existMoreEvents)
                events.RemoveAt(count);

            return new EventBlock(events, existMoreEvents);
        }

        public List<Category> GetCategories()
        {
            return CategoryDao.GetAllCategories();
        }

		public EventBlock FindEvents(string keys, int startIndex, int count)
		{
			return FindEvents(keys, null, startIndex, count);
		}

		public int GetNumberOfEvents(String keys, long? categoryId)
		{
			String[] keywords = null;
			if (keys != null && keys != "")
			{
				keywords = keys.Split(' ');
			}

			return EventDao.GetNumberOfEvents(keywords, categoryId);

		}

	}
}
