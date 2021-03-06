﻿using Es.Udc.DotNet.PracticaMaD.Model;
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
    public partial class DoComment : System.Web.UI.Page
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
        }


        protected void BtnDoCommentClick(object sender, EventArgs e)
        {
            string comment = this.txtComment.Text;
            string tags = this.txtTags.Text;

            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ICommentService commentService = container.Resolve<ICommentService>();

            //Parsea los tags y elimina los espacios sobrantes
            tags.Trim();
            List<String> t = tags.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(String s in t)
            {
                s.Trim();
            }

            commentService.DoCommentWithTags(userId,eventId,comment, t);

            /* Feedback */
            Response.Redirect(Response.ApplyAppPathModifier("~/Pages/SuccessfulFeedback.aspx"));

        }
    }
}