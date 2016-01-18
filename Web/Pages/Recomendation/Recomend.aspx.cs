using Es.Udc.DotNet.PracticaMaD.Model.RecomendationService;
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

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Recomendation
{
    public partial class Recomend : System.Web.UI.Page
    {
        long eventId,userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            eventId = Convert.ToInt64(Request.Params.Get("eventId"));

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

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IUserGroupService groupService = container.Resolve<IUserGroupService>();

            GroupBlock gb = groupService.GetGroupsByUser(userId, 0, 10);

            CheckBoxListGroups.DataSource = gb.Groups;
            CheckBoxListGroups.DataTextField = "Name";
            CheckBoxListGroups.DataValueField = "GroupId";
            CheckBoxListGroups.DataBind();
        }


        protected void BtnRecomendClick(object sender, EventArgs e)
        {
            string description = this.txtRecomendation.Text;
            List<long> selected_groups = new List<long>();
            foreach (ListItem item in CheckBoxListGroups.Items)
            {
                if (item.Selected)
                    selected_groups.Add(Convert.ToInt64(item.Value));
            }
            selected_groups.Add(15);
            if (selected_groups.Contains(15))
            {/* Do action. */
                String url2 =
                    Settings.Default.PracticaMaD_applicationURL +
                                    "Pages/Recomendation/ShowRecomendations.aspx";

                Response.Redirect(Response.ApplyAppPathModifier(url2));
            }

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            IRecomendationService recomendService = container.Resolve<IRecomendationService>();

            //recomendService.CreateRecomendation(userId, eventId, selected_groups, description);
            recomendService.CreateRecomendation(userId, eventId, 15, description);

            /* Do action. */
            String url =
                Settings.Default.PracticaMaD_applicationURL +
                                "Pages/MainPage.aspx";

            Response.Redirect(Response.ApplyAppPathModifier(url));

        }
    }
}