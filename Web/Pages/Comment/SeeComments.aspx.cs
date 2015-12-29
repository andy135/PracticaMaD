using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
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
    public partial class SeeComments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int startIndex, count;

            /* Get the start date (without time) */
            long eventId = Convert.ToInt64(Request.Params.Get("eventId"));



            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            ICommentService commentService =
                container.Resolve<ICommentService>();

            /* Get Events Info */
            CommentBlock commentBlock =
                commentService.GetCommentsOfEvent(eventId, startIndex, count);

            gvComments.DataSource = commentBlock.Comments;
            gvComments.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Comment/SeeComments.aspx" + "?eventId=" + eventId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (commentBlock.ExistMoreComments)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Comment/SeeComments.aspx" + "?eventId=" + eventId +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }
    }
    
}