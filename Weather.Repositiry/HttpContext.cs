using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Weather.Repositiry.Interfeice;

namespace Wether.Repository.Data
{
    public class HttpContext : IRepositoryWeather
    {
        private readonly string url;

        private readonly string key;

        private readonly HttpClient http;

        private bool disposed = false;

        public HttpContext(string url, string key)
        {
            this.url = url;
            this.key = key;
            http = new HttpClient();
        }

        public async Task<string> ReadUrlAsync(string city)
        {
            string urlWithParam = url + "?q=" + city + key;             
            HttpResponseMessage response = await http.GetAsync(urlWithParam);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;            
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    http.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
