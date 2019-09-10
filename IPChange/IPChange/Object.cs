using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChange
{
    class Object
    {
        List<string> listName = new List<string>();
        List<string> listDescription = new List<string>();
        List<string> listOperationalStatus = new List<string>();
        public List<string> ListDescription
        {
            get
            {
                return listDescription;
            }
            set
            {
                listDescription = value;
            }
        }
        public List<string> ListName
        {
            get
            {
                return listName;
            }
            set
            {
                listName = value;
            }
        }

        public List<string> ListOperationalStatus
        {
            get
            {
                return listOperationalStatus;
            }
            set
            {
                listOperationalStatus = value;
            }
        }

        public string LanCardName { get; set; }
        public string IPAddress { get; set; }
        public string SubNetMask { get; set; }
        public string GateWay { get; set; }
        public int Check { get; set; }
    }
}
