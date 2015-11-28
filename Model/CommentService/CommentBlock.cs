using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
	public class CommentBlock
    {
        public List<CommentInfo> Comments { get; private set; }
        public bool ExistMoreComments { get; private set; }

        public CommentBlock(List<CommentInfo> comments, bool existMoreComments)
        {
            this.Comments = comments;
            this.ExistMoreComments = existMoreComments;
        }
    }
}
