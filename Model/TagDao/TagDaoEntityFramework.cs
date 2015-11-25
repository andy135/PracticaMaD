using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
