using Core.DAL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Services
{
    public class EnvironmentContextService : IEnvironmentContext
    {
        private readonly IHostingEnvironment _env;

        public EnvironmentContextService(IHostingEnvironment env)
        {
            _env = env;
        }

        public string BaseUrl()
        {
            string url;
            switch (_env.EnvironmentName.ToLower())
            {
                case "production":
                    url = "";
                    break;
                case "staging":
                    url = "";
                    break;
                case "development":
                    url = "http://localhost:56564";
                    break;
                default:
                    url = "";
                    break;
            }
            return url;
        }

        public string Environment()
        {
            return _env.EnvironmentName;
        }
    }
}
