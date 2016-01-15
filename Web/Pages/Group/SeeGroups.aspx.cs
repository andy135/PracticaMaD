using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
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
    public partial class SeeGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblNoGroups.Visible = false;
            int startIndex, count;

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
            IUserGroupService groupService =
                container.Resolve<IUserGroupService>();

            /* Get Events Info */
            GroupBlock groupBlock =
                groupService.GetAllGroups(startIndex, count);

            if (groupBlock.Groups.Count == 0)
            {
                lblNoGroups.Visible = true;
                return;
            }

            gvGroups.DataSource = groupBlock.Groups;
            gvGroups.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "Pages/Group/SeeGroups.aspx" +
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
                    "Pages/Group/SeeGroups.aspx" +
                    "?startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }

        }
    }
}