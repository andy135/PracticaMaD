using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
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
			DbSet<Tag> tags = Context.Set<Tag>();

			try
			{
				Tag tag =
					(from t in tags
					 where t.tagName == text
					 select t).Single();

				return tag;
			}
			catch (System.InvalidOperationException)
			{
				return null;
			}
				
		}

		public List<Tag> GetAllTags()
		{
			return GetAllElements();
		}
	}
}
