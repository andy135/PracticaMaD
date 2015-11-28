using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;

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
		}
	}
}