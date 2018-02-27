using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace EventDetails
{
    class Submit
    {
        public async static Task<ResponseObject> PostAsJsonAsync(string uri, Details d, string token)
        {
            var itemJson = JsonConvert.SerializeObject(d);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var authValue = new AuthenticationHeaderValue(token);
            var http = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            var response = await http.PostAsync(uri, content);
            var status = response.IsSuccessStatusCode;

            ResponseObject data = new ResponseObject();
            if (status == true)
            {
                data.status = true;
                return data;
            }
            else
            {
                data.status = false;
                return data;
            }
        }

        public async static Task<ResponseObject> PutAsJsonAsync(string uri, EditObject d, string token)
        {
            var itemJson = JsonConvert.SerializeObject(d);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var authValue = new AuthenticationHeaderValue(token);
            var http = new HttpClient()
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            var response = await http.PutAsync(uri, content);
            var status = response.IsSuccessStatusCode;

            ResponseObject data = new ResponseObject();
            if (status == true)
            {
                data.status = true;
                return data;
            }
            else
            {
                data.status = false;
                return data;
            }
        }
    }

    [DataContract]
    public class ResponseObject
    {
        [DataMember]
        public bool status { get; set; }
    }
}