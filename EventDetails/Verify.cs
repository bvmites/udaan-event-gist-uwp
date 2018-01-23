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
    public class Verify
    {
        public async static Task<RootObject> PostAsJsonAsync(string uri, User item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var http = new HttpClient();
            var response = await http.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);
            return data;
        }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public bool status { get; set; }

        [DataMember]
        public string token { get; set; }
    }
}