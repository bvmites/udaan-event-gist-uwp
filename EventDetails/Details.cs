using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDetails
{
    public class Details
    {
        string eventName;
        string token;
        string type, department, tagline, description;
        int numOfParticipants, fees;
        

        string[] managerName;
        string[] phone;
        string[] round;

        public string Token { get => token; set => token = value; }
        public string Type { get => type; set => type = value; }
        public string Department { get => department; set => department = value; }
        public string EventName { get => eventName; set => eventName = value; }
        public string Tagline { get => tagline; set => tagline = value; }
        public string Description { get => description; set => description = value; }
        public int NumOfParticipants { get => numOfParticipants; set => numOfParticipants = value; }
        public int Fees { get => fees; set => fees = value; }     

        public string[] ManagerName { get => managerName; set => managerName = value; }
        public string[] Phone { get => phone; set => phone = value; }
        public string[] Round { get => round; set => round = value; }
    }
}
