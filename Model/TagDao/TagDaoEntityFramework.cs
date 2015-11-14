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
            Tag tagt = null;

            DbSet<Tag> tag = Context.Set<Tag>();

            string sqlQuery = "Select * FROM Tag where tagName=@tagName";
            DbParameter loginNameParameter =
                new System.Data.SqlClient.SqlParameter("tagName", text);

            tagt = Context.Database.SqlQuery<Tag>(sqlQuery, loginNameParameter).FirstOrDefault<Tag>();

            if (tagt == null)
                throw new InstanceNotFoundException(text,
                    typeof(Tag).FullName);

            return tagt;

        }

		public List<Tag> GetAllTags()
		{
			return GetAllElements();
		}
	}
}
