using Es.Udc.DotNet.PracticaMaD.Model.EventService;
using Es.Udc.DotNet.PracticaMaD.Web.Properties;
using Microsoft.Practices.Unity;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Es.Udc.DotNet.PracticaMaD.Web.Pages.Event
{
	public partial class FoundEvents : System.Web.UI.Page
	{
		ObjectDataSource evDataSource = new ObjectDataSource();

		protected void Page_Load(object sender, EventArgs e)
		{
			evDataSource.ObjectCreating += this.PbpDataSource_ObjectCreating;

			evDataSource.TypeName =
				 Settings.Default.ObjectDS_Events_Service;


			evDataSource.EnablePaging = true;

			evDataSource.SelectMethod =
				Settings.Default.ObjectDS_Events_SelectMethod;

			/* Get Account Identifier */
			String keys = Convert.ToString(Request.Params.Get("keys"));

			/* Get the start date (without time) */
			long categoryId = Convert.ToInt64(Request.Params.Get("categoryId"));

			evDataSource.SelectParameters.Add("keys", DbType.String, keys);
			evDataSource.SelectParameters.Add("categoryId", DbType.Int64, categoryId.ToString());

			evDataSource.SelectCountMethod =
				Settings.Default.ObjectDS_Events_CountMethod;
			evDataSource.StartRowIndexParameterName =
				Settings.Default.ObjectDS_Events_StartIndexParameter;
			evDataSource.MaximumRowsParameterName =
				Settings.Default.ObjectDS_Events_CountParameter;

			gvEvents.AllowPaging = true;
			gvEvents.PageSize = Settings.Default.PracticaMaD_defaultCount;

			gvEvents.DataSource = evDataSource;
			gvEvents.DataBind();
		}

		protected void GvEventsPageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvEvents.PageIndex = e.NewPageIndex;
			gvEvents.DataBind();
		}

		protected void PbpDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
		{

			/* Get the Service */
			IUnityContainer container = (IUnityContainer)
				HttpContext.Current.Application["unityContainer"];

			// Unity resolution of accountService produces a proxy to AccountService, 
			// so it should be forced to IAccountService to avoid errors with the type
			// declared in evDataSource.TypeName
			// 
			// IAccountService accountService = container.Resolve<IAccountService>();
			//
			// So, another option relies in a direct instantiaton of AccountService and the
			// use of BuildUp instance to complete the dependence
			//
			IEventService eventService = new EventService();
			eventService = (IEventService)
				container.BuildUp(eventService.GetType(),
								  eventService, "IEventService");

			e.ObjectInstance = (IEventService)eventService;

		}

	}
}