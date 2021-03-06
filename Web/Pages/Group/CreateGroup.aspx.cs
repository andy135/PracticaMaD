﻿using Es.Udc.DotNet.PracticaMaD.Model.UserGroupService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Group
{
    public partial class CreateGroup : System.Web.UI.Page
    {
        IUserGroupService groupService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            groupService = container.Resolve<IUserGroupService>();
        }

        protected void BtnCreateGroupClick(object sender, EventArgs e)
        {
            string name = this.groupName.Text;
            string description = this.description.Text;
            
            if (groupService != null && SessionManager.IsUserAuthenticated(Context))
            {
                long userId = SessionManager.GetUserSession(Context).UserProfileId;
                long groupId = groupService.CreateGroup(userId, name, description);

                groupService.SubscribeUserToGroup(userId, groupId);

                /* Feedback */
                Response.Redirect(Response.ApplyAppPathModifier("~/Pages/SuccessfulFeedback.aspx"));
            }
            else
            {
                //Insertar error aqui
            }

        }
    }
}