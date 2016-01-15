using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using System;
using System.Web.Security;
using System.Web.UI;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
	public partial class Authentication : System.Web.UI.Page
	{

        long? eventId = null;

        protected void Page_Load(object sender, EventArgs e)
		{

            lblPasswordError.Visible = false;
            lblLoginError.Visible = false;

            try
            {
                eventId = Convert.ToInt64(Request.Params.Get("eventId"));
            }
            catch (FormatException)
            {
                eventId = null;
            }
        }

		protected void BtnLoginClick(object sender, EventArgs e)
		{
			
			if (Page.IsValid)
			{
				try
				{
					SessionManager.Login(Context, txtLogin.Text,
						txtPassword.Text, checkRememberPassword.Checked);

                    FormsAuthentication.
                        RedirectFromLoginPage(txtLogin.Text,
                            checkRememberPassword.Checked);

                    if (eventId != null && eventId>0)
                    {
                        String url =
                            Settings.Default.PracticaMaD_applicationURL +
                                            "Pages/Comment/DoComment.aspx" + "?eventId=" +eventId;

                        Response.Redirect(Response.ApplyAppPathModifier(url));
                    }

				}
				catch (InstanceNotFoundException)
				{
					lblLoginError.Visible = true;
				}
				catch (IncorrectPasswordException)
				{
					lblPasswordError.Visible = true;
				}

			}
			
		}
	}
}