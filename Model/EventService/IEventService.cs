using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventService
{
    public interface IEventService
    {

        [Dependency]
        IEventDao EventDao { set; }


        EventBlock FindEvents(String keys, long? categoryId, int startIndex, int count);

		EventBlock FindEvents(String keys, int startIndex, int count);

		List<Category> GetCategories();


    }
}
