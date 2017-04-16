using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem
{
    public class CheckInAndOutInfo
    {
        public string CNICNumber { get; set; }

        public string CardNumber { get; set; }

        public string VehicleNmuber { get; set; }

        public decimal NoOfMaleGuest { get; set; }

        public decimal NoOfFemaleGuest { get; set; }

        public decimal DurationOfStay { get; set; }

        public decimal NoOfChildren { get; set; }

        public string AreaOfVisit { get; set; }

        public string HostName { get; set; }

        public DateTime DateTimeIn { get; set; }

        public DateTime DateTimeOut { get; set; }
        

        public bool CheckedIn { get; set; }
    }
}
