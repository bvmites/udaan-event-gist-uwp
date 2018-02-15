using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDetails
{
    public class Managers
    {
        public string name { get; set; }
        public string phone { get; set; }
    }

    public class Details
    {
        public string name { get; set; }
        public string type { get; set; }
        public string department { get; set; }
        public string tagline { get; set; }
        public string description { get; set; }
        public int teamSize { get; set; }
        public int fees { get; set; }
        public List<Managers> managers { get; set; }
        public List<string> rounds { get; set; }
    }
}
