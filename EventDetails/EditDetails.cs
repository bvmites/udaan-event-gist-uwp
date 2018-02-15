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
    public class EditDetails
    {
        public async static Task<List<EditObject>> GetDetails(string token)
        {
            var itemJson = JsonConvert.SerializeObject(token);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var http = new HttpClient();
            string url = "http://demo5369589.mockable.io/edit";
            var response = await http.PostAsync(url,content);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(List<EditObject>));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (List<EditObject>)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class Manager
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string phone { get; set; }

        public static implicit operator Manager(List<Manager> v)
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class EditObject
    {
        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string department { get; set; }

        [DataMember]
        public string tagline { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int teamSize { get; set; }

        [DataMember]
        public int fees { get; set; }

        [DataMember]
        public List<Manager> managers { get; set; }

        [DataMember]
        public List<string> rounds { get; set; }
    }
}