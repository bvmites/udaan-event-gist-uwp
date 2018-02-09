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
        public async static Task<EditObject> GetDetails(string token)
        {
            var itemJson = JsonConvert.SerializeObject(token);
            var content = new StringContent(itemJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var http = new HttpClient();
            string url = "https://demo5369589.mockable.io/edit";
            var response = await http.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(EditObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (EditObject)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class EditObject
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public bool Status { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public string Tagline { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int NumOfParticipants { get; set; }

        [DataMember]
        public int Fees { get; set; }

        [DataMember]
        public List<string> ManagerName { get; set; }

        [DataMember]
        public List<string> Phone { get; set; }

        [DataMember]
        public List<string> Round { get; set; }
    }
}