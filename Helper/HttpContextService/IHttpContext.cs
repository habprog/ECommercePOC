using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Helper.HttpContexServices
{
    internal interface IHttpContext
    {
        Task<dynamic> Get(string endpointURL);
        Task<dynamic> Post(string endpointURL, dynamic obj);
        Task<dynamic> Put(string endpointURL, dynamic obj);
    }
}
