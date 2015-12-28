using Es.Udc.DotNet.PracticaMaD.Model.EventDao;
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
		protected void Page_Load(object sender, EventArgs e)
		{
            lblNoEvents.Visible = false;
            int startIndex, count;

            /* Get Account Identifier */
            String keys = Convert.ToString(Request.Params.Get("keys"));

			/* Get the start date (without time) */
			long categoryId = Convert.ToInt64(Request.Params.Get("categoryId"));

            /* Get Start Index */
            try
            {
                startIndex = Int32.Parse(Request.Params.Get("startIndex"));
            }
            catch (ArgumentNullException)
            {
                startIndex = 0;
            }

            /* Get Count */
            try
            {
                count = Int32.Parse(Request.Params.Get("count"));
            }
            catch (ArgumentNullException)
            {
                count = Settings.Default.PracticaMaD_defaultCount;
            }

            /* Get the Service */
            IUnityContainer container =
                (IUnityContainer)HttpContext.Current.
                    Application["unityContainer"];
            IEventService eventService =
                container.Resolve<IEventService>();

            /* Get Events Info */
            EventBlock eventBlock =
                eventService.FindEvents(keys, categoryId, startIndex,count);

            if (eventBlock.Events.Count == 0)
            {
                lblNoEvents.Visible = true;
                return;
            }

			gvEvents.DataSource = eventBlock.Events;
			gvEvents.DataBind();

            /* "Previous" link */
            if ((startIndex - count) >= 0)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/Event/FoundEvents.aspx" + "?keys=" + keys + "&categoryId="+ categoryId +
                    "&startIndex=" + (startIndex - count) + "&count=" +
                    count;

                this.lnkPrevious.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkPrevious.Visible = true;
            }

            /* "Next" link */
            if (eventBlock.ExistMoreEvents)
            {
                String url =
                    Settings.Default.PracticaMaD_applicationURL +
                    "/Pages/Event/FoundEvents.aspx" + "?keys=" + keys + "&categoryId=" + categoryId +
                    "&startIndex=" + (startIndex + count) + "&count=" +
                    count;

                this.lnkNext.NavigateUrl =
                    Response.ApplyAppPathModifier(url);
                this.lnkNext.Visible = true;
            }
        }

	}
}