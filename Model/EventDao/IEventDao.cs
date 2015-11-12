using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
    public interface IEventDao : IGenericDao<Event, Int64>
    {

        List<Event> FindEvents(String[] keywords, long? categoryId, int startIndex, int count);

    }
}
