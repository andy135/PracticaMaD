using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    class CommentDaoEntityFramework :
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        public List<Comment> SearchCommentsByEventId(long eventId, int startIndex, int count)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            List<Comment> result =
                (from g in comments
                 where g.Event.eventId==eventId
                 orderby g.date
                 select g).Skip(startIndex).Take(count).ToList();

            return result;
        }
    }
}
