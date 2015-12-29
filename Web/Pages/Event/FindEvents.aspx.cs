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
			
            if (!IsPostBack)
            {
                List<Category> cats = GetCategories();
                FillComboCategories(cats);
            }
            
		}

		private void FillComboCategories(List<Category> cats)
		{
			this.comboCategory.DataTextField = "categoryName";
			this.comboCategory.DataValueField = "categoryId";
			this.comboCategory.DataSource = cats;
			this.comboCategory.DataBind();
            this.comboCategory.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            this.comboCategory.SelectedIndex = 0;
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