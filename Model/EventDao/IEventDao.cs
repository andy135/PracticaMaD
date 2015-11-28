using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
	public interface IEventDao : IGenericDao<Event, Int64>
    {

        List<EventInfo> FindEvents(String[] keywords, long? categoryId, int startIndex, int count);

    }
}
