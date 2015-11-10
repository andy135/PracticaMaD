using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentBlock
    {
        public List<Comment> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<Comment> comments, bool existMoreComments)
        {
            this.Comments = comments;
            this.ExistMoreComments = existMoreComments;
        }
    }
}
