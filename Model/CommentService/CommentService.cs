﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public void AddTagToComment(long commentId, long tagId)
		{
			Comment c = CommentDao.Find(commentId);
			Tag t = TagDao.Find(tagId);

            if (t == null)
            {
                throw new InstanceNotFoundException(tagId, typeof(Tag).FullName);
            }
            if (c == null)
            {
                throw new InstanceNotFoundException(commentId, typeof(Comment).FullName);
            }
			c.Tag.Add(t);
		}

		public long CreateNewTag(string tag)
		{
            try
            {
                TagDao.FindTagByText(tag);

                throw new DuplicateInstanceException(tag,
                    typeof(Tag).FullName);

            }
            catch (InstanceNotFoundException)
            {
                Tag t = new Tag();
                t.tagName = tag;
                t.usedNum = 0;
                TagDao.Create(t);
                return t.tagId;
            }
		}

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

        public Comment DoComment(Comment comment)
        {
            CommentDao.Create(comment);
            return comment;
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
            Comment comment = CommentDao.Find(commentId);

            if (comment != null)
            {
                comment.texto = text;
                CommentDao.Update(comment);
            }
        }

		public void RemoveTagFromComment(long commentId, long tagId)
		{
			Comment c = CommentDao.Find(commentId);
			Tag t = TagDao.Find(tagId);

			if (t != null && c != null && c.Tag.Contains(t))
			{
				c.Tag.Remove(t);
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
