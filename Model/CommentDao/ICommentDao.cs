using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {

        List<CommentInfo> SearchCommentsByEventId(long eventId, int startIndex, int count);

    }
}
