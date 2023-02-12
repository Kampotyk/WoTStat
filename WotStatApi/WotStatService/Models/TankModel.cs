using System.Collections.Generic;

namespace WotStat.Models
{
    public class TankModel
    {
        public string Name { get; set; }
        public long BattleCount { get; set; }
        public long WinCount { get; set; }
        public double WinRatio { get; set; }
        public long WinsToDesiredPercent { get; set; }
        public Constants.Badge Badge { get; set; }
        public int GunMarks { get; set; }
        public List<string> MasteryStats { get; set; }
        public List<KeyValuePair<string,string>> GunMarksStats { get; set; }
    }
}
