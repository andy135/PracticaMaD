using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web
{
	public partial class PracticaMaD : System.Web.UI.MasterPage
	{
		public static readonly String USER_SESSION_ATTRIBUTE = "userSession";

        protected void Page_Load(object sender, EventArgs e)
		{
			if (!SessionManager.IsUserAuthenticated(Context))
			{

				if (lnkUpdate != null)
					lnkUpdate.Visible = false;
				if (lnkLogout != null)
					lnkLogout.Visible = false;

			}
			else
			{
				if (lblWelcome != null)
					lblWelcome.Text =
						GetLocalResourceObject("lblWelcome.Hello.Text").ToString()
						+ " " + SessionManager.GetUserSession(Context).FirstName;
				if (lnkAuthenticate != null)
					lnkAuthenticate.Visible = false;
			}

            GenerateTagCloud();

        }

        private void GenerateTagCloud()
        {
            IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
            ICommentService commentService = container.Resolve<ICommentService>();

            List<Tag> tags = commentService.GetTopNTags(10);

            tags = ShuffleList(tags);

            long totalComments = commentService.GetNumOfComments();

            foreach (Tag t in tags)
            {
                string tagInUrl = Server.UrlEncode(t.tagName);
                HyperLink link = new HyperLink();
                link.Text = t.tagName;
                link.NavigateUrl = String.Format("~/Pages/Comment/ShowCommentsByTag.aspx?tag={0}", t.tagId);
                link.CssClass = GetCssClass(t.usedNum, totalComments);
                TagsPlaceHolder.Controls.Add(link);
                TagsPlaceHolder.Controls.Add(new LiteralControl("  "));
            }
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        private string GetCssClass(long tagCount, long commentCount)
        {
            long result = tagCount;// (tagCount * 100) / commentCount;
            if (result <= 20)
                return "TagSize1";
            if (result <= 40)
                return "TagSize2";
            if (result <= 60)
                return "TagSize3";
            if (result <= 80)
                return "TagSize4";
            if (result <= 100)
                return "TagSize5";
            else
                return "TagSize2";
        }
    }
}