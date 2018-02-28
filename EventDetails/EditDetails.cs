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
            var authValue = new AuthenticationHeaderValue(token);
            var http = new HttpClient()
            {
                DefaultRequestHeaders = {Authorization = authValue}
            };
            string url = "http://udaan18-events-api.herokuapp.com/events";
            var response = await http.GetAsync(url);
            var status = response.IsSuccessStatusCode;

            if (status == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                var serializer = new DataContractJsonSerializer(typeof(List<EditObject>));

                var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
                var data = (List<EditObject>)serializer.ReadObject(ms);

                return data;
            }
            else
                return null;
        }
    }

    [DataContract]
    public class Manager
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string phone { get; set; }
    }

    [DataContract]
    public class EditObject
    {
        [DataMember]
        public string _id { get; set; }

        [DataMember]
        public string eventName { get; set; }

        [DataMember]
        public string eventType { get; set; }

        [DataMember]
        public string department { get; set; }

        [DataMember]
        public string tagline { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public int teamSize { get; set; }

        [DataMember]
        public int entryFee { get; set; }

        [DataMember]
        public List<int> prizeMoney { get; set; }

        [DataMember]
        public List<Manager> managers { get; set; }

        [DataMember]
        public List<string> rounds { get; set; }
    }
}