using System.Web;
using System.Web.Optimization;

namespace UFSCar.BD.API
{
    public class BundleConfig
    { 
        public static void RegisterBundles(BundleCollection bundles)
        { 
            BundleTable.EnableOptimizations = true;
        }
    }
}
