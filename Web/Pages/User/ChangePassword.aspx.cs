using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using System;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
	public partial class ChangePassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			lblOldPasswordError.Visible = false;
		}

		protected void BtnChangePasswordClick(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				try
				{

					SessionManager.ChangePassword(Context, txtOldPassword.Text,
						txtNewPassword.Text);

					Response.Redirect(Response.
						ApplyAppPathModifier("~/Pages/MainPage.aspx"));

				}
				catch (IncorrectPasswordException)
				{
					lblOldPasswordError.Visible = true;
				}
			}
		}

	}
}