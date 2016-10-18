using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFSCar.DB.UI.Models
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigurarAPIs();
        }

        private void ConfigurarAPIs()
        {
            hfURLAPITSE.Value = System.Configuration.ConfigurationManager.AppSettings["API.TSE"];
        }
    }
}