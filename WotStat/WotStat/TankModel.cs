using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotStat
{
    class TankModel
    {
        public string name { get; set;}
        public int battleCount { get; set; }
        public float winRatio { get; set; }
        public int battlesTo50Percent { get; set; }
    }
}
