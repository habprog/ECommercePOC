using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Helper.HttpContexServices
{
    public class HttpContext : IHttpContext
    {
        private readonly IConfiguration _config;

        string url = string.Empty;
        dynamic jsonObj = new JObject();

        private readonly ISession session;

        HttpClient client = new HttpClient();
        public HttpContext(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            url = _config["Base_URL"];
            session = httpContextAccessor.HttpContext.Session;
        }
        public async Task<dynamic> Get(string endpointURL)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                //var token = this.session.GetString("Token");
                //client.DefaultRequestHeaders.Add("Authorization",
                //    "Bearer " + token);

                var result = await client.GetAsync(url + endpointURL).ConfigureAwait(false);

                var responJsonText = await result.Content.ReadAsStringAsync();
                var res = new JObject();
                if (result.IsSuccessStatusCode)
                {
                    res = JsonConvert.DeserializeObject<JObject>(Convert.ToString(responJsonText));
                    jsonObj.success = Convert.ToBoolean(res["success"]);
                    var data = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(res["data"]));
                    jsonObj.data = data != null ? data : Convert.ToString(res["message"]);
                }
                else
                {
                    jsonObj.success = false;
                    jsonObj.message = result.StatusCode;
                }
                return jsonObj;
            }
        }

        public async Task<dynamic> Post(string endpointURL, dynamic obj)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                //var token = this.session.GetString("Token");
                //client.DefaultRequestHeaders.Add("Authorization",
                //    "Bearer " + token);
                using (var stringContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"))
                {
                    var result = await client.PostAsync(url + endpointURL, stringContent);
                    string responJsonText = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        var res = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(responJsonText));
                        var success = Convert.ToBoolean(res["success"]);
                        if (success)
                        {
                            jsonObj.success = success;
                            jsonObj.data = Convert.ToString(res["data"]);
                            jsonObj.message = Convert.ToString(res["message"]);
                        }
                        else
                        {
                            jsonObj.success = success;
                            jsonObj.data = Convert.ToString(res["message"]);
                        }

                        if (result.Headers.Contains("access_token")) { jsonObj.TokenObj = result.Headers.GetValues("access_token").First(); }
                    }
                    else
                    {
                        jsonObj.success = false;
                        jsonObj.message = result.StatusCode;
                    }
                    return jsonObj;
                }
            }
        }

        public async Task<dynamic> Put(string endpointURL, dynamic obj)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                //var token = this.session.GetString("Token");
                //client.DefaultRequestHeaders.Add("Authorization",
                //    "Bearer " + token);

                using (var stringContent = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"))
                {
                    var result = await client.PutAsync(url + endpointURL, stringContent);
                    string responJsonText = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        var res = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(responJsonText));
                        var success = Convert.ToBoolean(res["success"]);
                        if (success)
                        {
                            jsonObj.success = success;
                            jsonObj.data = Convert.ToString(res["data"]);
                            jsonObj.message = Convert.ToString(res["message"]);
                        }
                        else
                        {
                            jsonObj.success = false;
                            jsonObj.data = Convert.ToString(res["message"]);
                        }
                    }
                    else
                    {
                        jsonObj.success = false;
                        jsonObj.message = result.StatusCode;
                    }
                    return jsonObj;
                }
            }
        }
    }
}
