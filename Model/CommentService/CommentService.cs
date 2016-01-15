using System;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Microsoft.Practices.Unity;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
	public class CommentService : ICommentService
    {
        [Dependency]
        public ICommentDao CommentDao { private get; set; }

		[Dependency]
		public ITagDao TagDao { private get; set; }

		public Tag ManageTag(String tag)
		{
			try
			{
				Tag t = TagDao.FindTagByText(tag);
				t.usedNum++;
				TagDao.Update(t);
				return t;
			}
			catch (InstanceNotFoundException)
			{
				Tag t = new Tag();
				t.tagName = tag;
				t.usedNum = 0;
				TagDao.Create(t);
				return t;
			}
		}

		public void DeleteComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

		public long DoComment(long userId, long eventId, String text)
		{
			return DoCommentWithTags(userId,eventId,text, null);
		}

		public long DoCommentWithTags(long userId, long eventId, String text, List<String> tags)
		{
			Comment comment = new Comment();
			comment.texto = text;
			comment.eventId = eventId;
			comment.usrId = userId;
            comment.date = DateTime.Now;
			CommentDao.Create(comment);
			if (tags != null) { 
				foreach(String t in tags)
				{
					Tag tag = ManageTag(t);
					CommentDao.AddTagToComment(comment.commentId, tag);
				}
			}

			return comment.commentId;
		}

		public List<Tag> GetAllTags()
		{
			return TagDao.GetAllTags();
		}

		public CommentBlock GetCommentsOfEvent(long eventId, int startIndex, int count)
        {
            List<CommentInfo> comments = CommentDao.SearchCommentsByEventId(eventId, startIndex, count + 1);

            bool existMoreComments = (comments.Count == count + 1);

            if (existMoreComments)
                comments.RemoveAt(count);

            return new CommentBlock(comments, existMoreComments);
        }

        public void ModifyComment(long commentId, string text)
        {
			ModifyCommentWithTags(commentId, text, null);
        }

		public void ModifyCommentWithTags(long commentId, string text, List<String> tags)
		{
			Comment comment = CommentDao.Find(commentId);

			if (comment != null)
			{
				comment.texto = text;
				
				if (tags != null)
				{
					CommentDao.RemoveTagsFromComment(commentId);
					foreach (String t in tags)
					{
						Tag tag = ManageTag(t);
						CommentDao.AddTagToComment(comment.commentId, tag);
					}
				}
				CommentDao.Update(comment);
			}
		}

        public Tag GetTagById(long tagId)
        {
            return TagDao.Find(tagId);
        }

		public CommentBlock GetCommentsByTag(long tagId, int startIndex, int count)
		{
			List<CommentInfo> comments = CommentDao.SearchCommentsByTag(tagId, startIndex, count + 1);

			bool existMoreComments = (comments.Count == count + 1);

			if (existMoreComments)
				comments.RemoveAt(count);

			return new CommentBlock(comments, existMoreComments);
		}

		public List<Tag> GetTopNTags(int n)
		{
			return TagDao.GetTopNTags(n);
		}
	}
}
