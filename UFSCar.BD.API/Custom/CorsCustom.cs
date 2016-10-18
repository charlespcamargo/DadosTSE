using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http.Cors;


namespace UFSCar.BD.API.Custom
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class CorsCustom : Attribute, ICorsPolicyProvider
    {
        private CorsPolicy _policy;

        public CorsCustom()
        {
            // Create a CORS policy.
            _policy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = true
            };

            if (ConfigurationManager.AppSettings["CORS.API"] != null)
            {
                // Add allowed origins.
                string[] cors = ConfigurationManager.AppSettings["CORS.API"].Split(',');
                foreach (var item in cors)
                {
                    _policy.Origins.Add(item);
                }
            }
            else
            {
                _policy.Origins.Add("*");
            }
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request)
        {
            return Task.FromResult(_policy);
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            return Task.FromResult(_policy);
        }
    }

    public class CorsPolicyFactory : ICorsPolicyProviderFactory
    {
        ICorsPolicyProvider _provider = new CorsCustom();

        public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
        {
            return _provider;
        }
    } 

}