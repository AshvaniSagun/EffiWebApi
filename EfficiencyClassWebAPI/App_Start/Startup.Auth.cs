using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.IdentityModel.Tokens;

namespace EfficiencyClassWebAPI
{
    public partial class Startup
    {
        //2018-06-04 SPHU: The app services client 
        public static string audience = ConfigurationManager.AppSettings["AUDIENCE"];
        public static string tenant = ConfigurationManager.AppSettings["TENANT"];
        public void ConfigureAuth(IAppBuilder app)
        {                      
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Tenant = tenant,
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = audience
                    }

                    //#if DEBUG
                    //                            ,
                    //                    BackchannelHttpHandler = new HttpClientHandler { Proxy = new WebProxy("http://proxy:83") }
                    //#endif
                });           
        }
    }
}