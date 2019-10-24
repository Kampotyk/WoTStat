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
    }
}
