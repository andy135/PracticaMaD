using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
	public class CommentInfo
	{
		public long commentId { get; set; }
		public long usrId { get; set; }
		public long eventId { get; set; }
		public System.DateTime date { get; set; }
		public string texto { get; set; }
		public string userName { get; set; }

		public CommentInfo(long commentId, long usrId, long eventId, DateTime date, string texto, string userName)
		{
			this.commentId = commentId;
			this.usrId = usrId;
			this.eventId = eventId;
			this.date = date;
			this.texto = texto;
			this.userName = userName;
		}
	}
}
