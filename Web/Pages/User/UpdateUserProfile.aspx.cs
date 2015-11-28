using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.View.ApplicationObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.User
{
	public partial class UpdateUserProfile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				UserProfileDetails userProfileDetails =
					SessionManager.FindUserProfileDetails(Context);

				txtFirstName.Text = userProfileDetails.FirstName;
				txtSurname.Text = userProfileDetails.Lastname;
				txtEmail.Text = userProfileDetails.Email;

				/* Combo box initialization */
				UpdateComboLanguage(userProfileDetails.Language);
				UpdateComboCountry(userProfileDetails.Language,
					userProfileDetails.Country);
			}
		}

		private void UpdateComboLanguage(String selectedLanguage)
		{
			this.comboLanguage.DataSource = Languages.GetLanguages(selectedLanguage);
			this.comboLanguage.DataTextField = "text";
			this.comboLanguage.DataValueField = "value";
			this.comboLanguage.DataBind();
			this.comboLanguage.SelectedValue = selectedLanguage;
		}

		private void UpdateComboCountry(String selectedLanguage, String selectedCountry)
		{
			this.comboCountry.DataSource = Countries.GetCountries(selectedLanguage);
			this.comboCountry.DataTextField = "text";
			this.comboCountry.DataValueField = "value";
			this.comboCountry.DataBind();
			this.comboCountry.SelectedValue = selectedCountry;
		}

		protected void BtnUpdateClick(object sender, EventArgs e)
		{

			if (Page.IsValid)
			{
				UserProfileDetails userProfileDetails =
					new UserProfileDetails(txtFirstName.Text, txtSurname.Text,
						txtEmail.Text, comboLanguage.SelectedValue,
						comboCountry.SelectedValue);

				SessionManager.UpdateUserProfileDetails(Context,
					userProfileDetails);

				Response.Redirect(
					Response.ApplyAppPathModifier("~/Pages/MainPage.aspx"));

			}
		}

		protected void ComboLanguageSelectedIndexChanged(object sender, EventArgs e)
		{
			/* After a language change, the countries are printed in the
			 * correct language.
			 */
			this.UpdateComboCountry(comboLanguage.SelectedValue,
				comboCountry.SelectedValue);
		}
	}
}