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
        public async static Task<ResponseObject> PostAsJsonAsync(string uri, Details d)
        {
            var itemJson = JsonConvert.SerializeObject(d);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var http = new HttpClient();
            var response = await http.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(ResponseObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (ResponseObject)serializer.ReadObject(ms);
            return data;
        }

        public async static Task<ResponseObject> PostAsJsonAsync(string uri, EditObject d)
        {
            var itemJson = JsonConvert.SerializeObject(d);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var http = new HttpClient();
            var response = await http.PutAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(ResponseObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (ResponseObject)serializer.ReadObject(ms);
            return data;
        }
    }

    [DataContract]
    public class ResponseObject
    {
        [DataMember]
        public bool status { get; set; }
    }
}