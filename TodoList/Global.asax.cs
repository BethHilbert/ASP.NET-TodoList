using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace TodoList
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
			AreaRegistration.RegisterAllAreas();

			// register megaphone Specific Views
			DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("megaphone")
			{
				ContextCondition = (context => context.Request.UserAgent
					.IndexOf("megaphone", StringComparison.OrdinalIgnoreCase) >= 0)
			});

			// register mobile Specific Views
			DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("mobile")
			{
				ContextCondition = (context => context.Request.UserAgent
					.IndexOf("mobile", StringComparison.OrdinalIgnoreCase) >= 0)
			});

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
