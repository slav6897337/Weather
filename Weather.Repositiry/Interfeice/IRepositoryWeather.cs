using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Repositiry.Interfeice
{
    public interface IRepositoryWeather : IDisposable
    {
        Task<string> ReadUrlAsync(string city);
    }
}
