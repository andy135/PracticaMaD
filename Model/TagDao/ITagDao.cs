using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
	public interface ITagDao : IGenericDao<Tag, Int64>
	{
		List<Tag> GetAllTags();

		List<Tag> GetTopNTags(int n);

		Tag FindTagByText(String text);
	}
}
