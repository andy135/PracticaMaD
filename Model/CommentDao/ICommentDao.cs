using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
	public interface ICommentDao : IGenericDao<Comment, Int64>
    {

        List<CommentInfo> SearchCommentsByEventId(long eventId, int startIndex, int count);

		List<CommentInfo> SearchCommentsByTag(long tagId, int startIndex, int count);

        long GetNumOfComments();

		void AddTagToComment(long commentId, Tag tag);

		void RemoveTagsFromComment(long commentId);

	}
}
