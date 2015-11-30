using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Web.HTTP.Session;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
	public partial class FindEvents : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			List<Category> cats = GetCategories();
			FillComboCategories(cats);
		}
		/*
		private void UpdateComboCategory(String selectedCategory)
		{
			this.comboCategory.DataSource = Languages.GetLanguages(selectedCategory);
			this.comboCategory.DataTextField = "text";
			this.comboCategory.DataValueField = "value";
			this.comboCategory.DataBind();
			this.comboCategory.SelectedValue = selectedCategory;
		}
		*/

		private void FillComboCategories(List<Category> cats)
		{
			this.comboCategory.DataTextField = "categoryName";
			this.comboCategory.DataValueField = "categoryId";
			this.comboCategory.DataSource = cats;
			this.comboCategory.DataBind();
		}

		private List<Category> GetCategories()
		{
			IUnityContainer container = (IUnityContainer)HttpContext.Current.Application["unityContainer"];
			IEventService eventService = container.Resolve<IEventService>();

			return eventService.GetCategories();
		}


		protected void BtnFindEventsClick(object sender, EventArgs e)
		{
			string keys = this.txtKeys.Text;
			string catId = this.comboCategory.SelectedValue;

			/* Do action. */
			String url =
				String.Format("./FoundEvents.aspx?keys={0}" +
					"&categoryId={1}", keys, catId);

			Response.Redirect(Response.ApplyAppPathModifier(url));

		}
	}
}