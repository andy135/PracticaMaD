using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Comment
{
    public partial class ModifyComment : System.Web.UI.Page
    {
        long eventId,userId,commentId;
        ICommentService commentService;

        protected void Page_Load(object sender, EventArgs e)
        {
            eventId = Convert.ToInt64(Request.Params.Get("eventId"));
            commentId = Convert.ToInt64(Request.Params.Get("commentId"));

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            commentService = container.Resolve<ICommentService>();

            if (!IsPostBack)
            {
                CommentInfo c = commentService.GetCommentById(commentId);
                List<Tag> tags = commentService.GetTagsByCommentId(commentId);

                string stags = ParseTags(tags);

                this.txtComment.Text = c.texto;
                this.txtTags.Text = stags;

            }
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                /* Do action. */
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                                    "Pages/User/Authentication.aspx" + "?eventId=" + eventId;

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            else
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }
        }

        private string ParseTags(List<Tag> tags)
        {
            string s = "";
            foreach(Tag t in tags)
            {
                s += "#" + t.tagName + " ";
            }
            return s.Trim();
        }

        protected void BtnDoCommentClick(object sender, EventArgs e)
        {
            string comment = this.txtComment.Text;
            string tags = this.txtTags.Text;

            //Parsea los tags y elimina los espacios sobrantes
            List<String> t = tags.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(String s in t)
            {
                s.Trim();
            }

            commentService.ModifyCommentWithTags(commentId, comment, t);

            /* Do action. */
            String url =
                Settings.Default.PracticaMaD_applicationURL +
                                "Pages/MainPage.aspx";

            Response.Redirect(Response.ApplyAppPathModifier(url));

        }
    }
}