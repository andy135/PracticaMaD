using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.RecomendationService
{
    [Serializable()]
    public class RecomendationInfo
    {
        public long RecomendationId { get; private set; }
        public DateTime Date { get; private set; }
        public String Description { get; private set; }
        public long EventId { get; private set; }
        public String EventName { get; private set; }
        public Boolean ExistComments { get; private set; }

        public RecomendationInfo(long rId, DateTime date, String desc, long eventId,String eventName, Boolean ec)
        {
            this.RecomendationId = rId;
            this.Date = date;
            this.Description = desc;
            this.EventId = eventId;
            this.EventName = eventName;
            this.ExistComments = ec;
        }
    }
}
