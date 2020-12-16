using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Wether.Repository.Data;



namespace Weather.Configurations
{
    public static class ConfigureHttpExstensions
    {
        public static void ConfigureHttp(this IServiceCollection services, string url, string key)
        {
            HttpContext context = new HttpContext(url, key);
            services.AddSingleton<HttpContext>(context);
        }
    }
}
