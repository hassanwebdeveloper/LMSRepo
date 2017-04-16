using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationManagementSystem
{
    public class BlockedPersonInfo
    {
        public string CNICNumber { get; set; }

        public string BlockedBy { get; set; }

        public string Reason { get; set; }

        public bool Blocked { get; set; }
    }
}
