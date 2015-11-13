using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void ModifyComment(long commentId, String text);

        [Transactional]
        void DeleteComment(long commentId);

        [Transactional]
        CommentBlock GetCommentsOfEvent(long eventId, int startIndex, int count);

		// TAGS SUPPORT

		[Transactional]
		void AddTagToComment(long commentId, long tagId);

		[Transactional]
		void RemoveTagFromComment(long commentId, long tagId);

		[Transactional]
		long CreateNewTag(String tag);

		[Transactional]
		List<Tag> GetAllTags();
	}
}
