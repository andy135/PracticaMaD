using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<CommentInfo> SearchCommentsByEventId(long eventId, int startIndex, int count)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            List<Comment> result =
                (from g in comments
                 where g.Event.eventId==eventId
                 orderby g.date
                 select g).Skip(startIndex).Take(count).ToList();

			List<CommentInfo> commentsinfo = new List<CommentInfo>();
			CommentInfo ci;
			foreach (Comment c in result)
			{
				ci = new CommentInfo(c.commentId,c.usrId,c.eventId,c.date,c.texto,c.UserProfile.loginName);
				commentsinfo.Add(ci);
			}

			return commentsinfo;
        }

		public List<CommentInfo> SearchCommentsByTag(long tagId, int startIndex, int count)
		{
			DbSet<Comment> comments = Context.Set<Comment>();
			DbSet<Tag> tags = Context.Set<Tag>();

			Tag tag = (from t in tags
					   where t.tagId == tagId
					   select t).SingleOrDefault();

			List<Comment> result =
				(from g in comments
				 from t in g.Tag
				 where g.Tag.Contains(tag)
				 orderby g.date
				 select g).Skip(startIndex).Take(count).ToList();

			List<CommentInfo> commentsinfo = new List<CommentInfo>();
			CommentInfo ci;
			foreach (Comment c in result)
			{
				ci = new CommentInfo(c.commentId, c.usrId, c.eventId, c.date, c.texto, c.UserProfile.loginName);
				commentsinfo.Add(ci);
			}

			return commentsinfo;
		}
	}
}
