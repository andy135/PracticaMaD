using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
	class TagDaoEntityFramework :
		GenericDaoEntityFramework<Tag, Int64>, ITagDao
	{
		public Tag FindTagByText(string text)
		{
            Tag tag = null;

            DbSet<Tag> tags = Context.Set<Tag>();

			tag =
				(from t in tags
				 where t.tagName == text
				 select t).FirstOrDefault<Tag>();

            if (tag == null)
                throw new InstanceNotFoundException(text,
                    typeof(Tag).FullName);

            return tag;

        }

		public List<Tag> GetAllTags()
		{
			return GetAllElements();
		}

		public List<Tag> GetTopNTags(int n)
		{
			DbSet<Tag> tags = Context.Set<Tag>();

			List<Tag> list =
				(from t in tags
				 orderby t.usedNum descending
				 select t).Take(n).ToList();

			return list;
		}
	}
}
