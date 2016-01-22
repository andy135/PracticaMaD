using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
	public interface ITagDao : IGenericDao<Tag, Int64>
	{
		List<Tag> GetAllTags();

		List<Tag> GetTopNTags(int n);

        List<Tag> GetTagsByCommentId(long commentId);

        /// <exception cref="InstanceNotFoundException"
        Tag FindTagByText(String text);
	}
}
