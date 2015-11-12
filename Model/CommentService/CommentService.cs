using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Microsoft.Practices.Unity;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        [Dependency]
        public ICommentDao CommentDao { private get; set; }

        public void DeleteComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

        public Comment DoComment(Comment comment)
        {
            CommentDao.Create(comment);
            return comment;
        }

		public long DoComment(long userId, long eventId, String text)
		{
			Comment comment = new Comment();
			comment.texto = text;
			comment.eventId = eventId;
			comment.usrId = userId;
			CommentDao.Create(comment);

			return comment.commentId;
		}

		public CommentBlock GetCommentsOfEvent(long eventId, int startIndex, int count)
        {
            List<Comment> comments = CommentDao.SearchCommentsByEventId(eventId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments)
                comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        public void ModifyComment(long commentId, string text)
        {
            Comment comment = CommentDao.Find(commentId);

            if (comment != null)
            {
                /*Modify comment text*/
                comment.texto = text;

                CommentDao.Update(comment);
            }
        }
    }
}
