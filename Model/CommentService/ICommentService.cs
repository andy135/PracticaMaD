using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
	public interface ICommentService
    {
        [Dependency]
        ICommentDao CommentDao { set; }

		[Dependency]
		ITagDao TagDao { set; }

		[Transactional]
        long DoComment(long userId, long eventId, String text);

		[Transactional]
		long DoCommentWithTags(long userId, long eventId, String text, List<String> tags);

		[Transactional]
        void ModifyComment(long commentId, String text);

		[Transactional]
		void ModifyCommentWithTags(long commentId, string text, List<String> tags);

		[Transactional]
        void DeleteComment(long commentId);

        [Transactional]
        CommentBlock GetCommentsOfEvent(long eventId, int startIndex, int count);

        [Transactional]
        long GetNumOfComments();

        // TAGS SUPPORT

        [Transactional]
		CommentBlock GetCommentsByTag(long tagId, int startIndex, int count);

		[Transactional]
		List<Tag> GetAllTags();

        [Transactional]
        Tag GetTagById(long tagId);

		[Transactional]
		List<Tag> GetTopNTags(int n);


	}
}
