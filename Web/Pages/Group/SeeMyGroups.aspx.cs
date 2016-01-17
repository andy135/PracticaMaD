using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class SeeMyGroups : System.Web.UI.Page
    {
        IUserGroupService groupService;
        long userId;
        int startIndex, count;

        protected void Page_Load(object sender, EventArgs e)
        {

            lblNoGroups.Visible = false;

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
            groupService =
                container.Resolve<IUserGroupService>();

            /* Get Events Info */
            if (SessionManager.IsUserAuthenticated(Context))
            {
                userId = SessionManager.GetUserSession(Context).UserProfileId;
            }

            GroupBlock groupBlock =
                groupService.GetGroupsByUser(userId, startIndex, count);

            if (groupBlock.Groups.Count == 0)
            {
                lblNoGroups.Visible = true;
                return;
            }

            gvMyGroups.DataSource = groupBlock.Groups;
            gvMyGroups.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Group/SeeMyGroups.aspx" +
                    "?startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (groupBlock.ExistMoreGroups)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Group/SeeMyGroups.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }

        protected void GridView_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvMyGroups.Rows)
            {
                long groupId = Convert.ToInt64(row.Cells[0].Text);
                if (row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lb = new LinkButton();
                    lb.Text = GetLocalResourceObject("signout.Text").ToString();
                    lb.CommandName = "darseBaja";
                    lb.CommandArgument = groupId.ToString();
                    lb.Command += LinkButton_Command;
                    row.Cells[4].Controls.Add(lb);
                }
            }
        }

        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "darseBaja")
            {
                LinkButton lb = (LinkButton)sender;
                long groupId = Convert.ToInt64(lb.CommandArgument);
                groupService.UnsubscribeUserToGroup(userId, groupId);

                GroupBlock groupBlock =
                    groupService.GetGroupsByUser(userId, startIndex, count);

                if (groupBlock.Groups.Count == 0)
                {
                    lblNoGroups.Visible = true;
                    return;
                }

                gvMyGroups.DataSource = groupBlock.Groups;
                gvMyGroups.DataBind();
            }
        }
    }
}