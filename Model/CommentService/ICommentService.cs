using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
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

        [Transactional]
        Comment DoComment(Comment comment);

        [Transactional]
        void ModifyComment(long commentId, String text);

        [Transactional]
        void DeleteComment(long commentId);

        [Transactional]
        CommentBlock GetCommentsOfEvent(long eventId, int startIndex, int count);

    }
}
