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
    public partial class SeeComments : System.Web.UI.Page
    {
        ICommentService commentService;
        int startIndex, count;
        long eventId;
        protected void Page_Load(object sender, EventArgs e)
        {

            /* Get the start date (without time) */
            eventId = Convert.ToInt64(Request.Params.Get("eventId"));


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
            commentService =
                container.Resolve<ICommentService>();

            /* Get Comments Info */
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
        protected void GridView_DataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow && SessionManager.IsUserAuthenticated(Context))
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;                
                CommentInfo c = (CommentInfo)e.Row.DataItem;
                if (c.usrId == userId)
                {
                    LinkButton lb = new LinkButton();
                    lb.Text = GetLocalResourceObject("modify.Text").ToString();
                    lb.CommandName = "modificar";
                    lb.CommandArgument = c.commentId.ToString();
                    lb.Command += LinkButton_Command;
                    e.Row.Cells[3].Controls.Add(lb);

                    lb = new LinkButton();
                    lb.Text = GetLocalResourceObject("remove.Text").ToString();
                    lb.CommandName = "eliminar";
                    lb.CommandArgument = c.commentId.ToString();
                    lb.Command += LinkButton_Command;
                    e.Row.Cells[4].Controls.Add(lb);
                }
            }            
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "eliminar" || e.CommandName == "modificar")
            {
                LinkButton lb = (LinkButton)sender;
                if(e.CommandName == "eliminar")
                {
                    commentService.DeleteComment(Convert.ToInt64(e.CommandArgument));

                    CommentBlock commentBlock =
                        commentService.GetCommentsOfEvent(eventId, startIndex, count);

                    gvComments.DataSource = commentBlock.Comments;
                    gvComments.DataBind();
                }
                if (e.CommandName == "modificar")
                {
                    long commentId = Convert.ToInt64(e.CommandArgument);

                    String url =
                    Settings.Default.PracticaMaD_applicationURL +
                                    "Pages/Comment/ModifyComment.aspx" + "?eventId=" + eventId + "&commentId=" +commentId;

                    Response.Redirect(Response.ApplyAppPathModifier(url));
                }
            }
        }
    }
    
}