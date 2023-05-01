using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    class BusData
    {
        public BusData(string busSerial, string busTiming, string busRoute, string date)
        {
            this.busSerial = busSerial;
            this.busTiming = busTiming;
            this.busRoute = busRoute;
            this.date = date;
        }
        public string busSerial;
        public string busTiming;
        public string busRoute;
        public string date;
        public BusData()
        {
            string busSerial;
            string busTiming;
            string busRoute;
            string date;
        }
    }
}
