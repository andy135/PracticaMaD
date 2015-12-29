using System;

namespace Es.Udc.DotNet.PracticaMaD.Model.EventDao
{
	public class EventInfo
	{
		public long EventId { get; set; }
		public string EventName { get; set; }
		public string Review { get; set; }
		public System.DateTime Date { get; set; }
		public long CategoryId { get; set; }
		public string CategoryName { get; set; }
		public long NumComments { get; set; }

		public EventInfo(long eventId, String eventName, String review, DateTime date, long categoryId, string categoryName,long numComments )
		{
			this.EventId = eventId;
			this.EventName = eventName;
			this.Review = review;
			this.Date = date;
			this.CategoryId = categoryId;
			this.CategoryName = categoryName;
			this.NumComments = numComments;
        }
	}
}
