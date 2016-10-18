using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation.Providers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UFSCar.BD.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Schedule de Serviços de importação
          //  FluentScheduler.TaskManager.UnobservedTaskException += TaskManager_UnobservedTaskException;
        }


        //private void TaskManager_UnobservedTaskException(FluentScheduler.Model.TaskExceptionInformation sender, UnhandledExceptionEventArgs e)
        //{
        //    Console.Write("Erro: " + e.ExceptionObject);
        //}
    }
}
