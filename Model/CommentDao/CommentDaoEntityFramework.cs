using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

			List<Comment> result =
				(from g in comments
				 from t in g.Tag
				 where g.Tag.Select(s => s.tagId).Contains(tagId)
				 orderby g.date
				 select g).Skip(startIndex).Take(count).ToList();

			List<CommentInfo> commentsinfo = new List<CommentInfo>();
			CommentInfo ci;
			foreach (Comment c in result)
			{
                if (c.UserProfile == null)
                {
                    ci = new CommentInfo(c.commentId, c.usrId, c.eventId, c.date, c.texto, "Null");
                }
                else {
                    ci = new CommentInfo(c.commentId, c.usrId, c.eventId, c.date, c.texto, c.UserProfile.loginName);
                }
				commentsinfo.Add(ci);
			}

			return commentsinfo;
		}

        public void AddTagToComment(long commentId,Tag tag)
		{
			Comment c = Find(commentId);
			if (c != null)
			{
				c.Tag.Add(tag);
			}
			Update(c);
		}

		public void RemoveTagsFromComment(long commentId)
		{
			Comment c = Find(commentId);
			if (c != null)
			{
				c.Tag.Clear();
			}
			Update(c);
		}

        public long GetNumOfComments()
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            long result = (from c in comments select c).Count();

            return result;
        }
    }
}
