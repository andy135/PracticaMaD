using Es.Udc.DotNet.PracticaMaD.Model.RecomendationService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recomendation
{
    public partial class ShowRecomendations : System.Web.UI.Page
    {
        IRecomendationService recomendationService;
        int startIndex, count;
        long userId;
        protected void Page_Load(object sender, EventArgs e)
        {

            /* Get the User ID */
            if (!SessionManager.IsUserAuthenticated(Context))
            {
                /* Do action. */
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                                    "Pages/User/Authentication.aspx";

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
            else
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }


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
            recomendationService =
                container.Resolve<IRecomendationService>();

            /* Get Comments Info */
            RecomendationBlock recBlock =
                recomendationService.GetRecomendationsByUser(userId, startIndex, count);

            gvRecomendations.DataSource = recBlock.Recomendations;
            gvRecomendations.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Recomendation/ShowRecomendations.aspx" +// "?userId=" + eventId +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (recBlock.ExistMoreRecomendations)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Recomendation/ShowRecomendations.aspx" + // "?eventId=" + eventId +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

        protected void GridView_DataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                RecomendationInfo r = (RecomendationInfo)e.Row.DataItem;
                if (r.ExistComments)
                { 
                    LinkButton lb = new LinkButton();
                    lb.Text = r.EventName;
                    lb.CommandArgument = r.EventId.ToString();
                    lb.CommandName = "comentarios";
                    lb.Command += LinkButton_Command;
                    e.Row.Cells[3].Controls.Add(lb);
                }
            }
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "comentarios")
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                                    "/Pages/Comment/SeeComments.aspx?eventId=" + Convert.ToInt64(e.CommandArgument);

                Response.Redirect(Response.ApplyAppPathModifier(url));
            }
        }
    }
}