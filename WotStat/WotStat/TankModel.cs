namespace WotStat
{
    internal class TankModel
    {
        public string Name { get; }
        public long BattleCount { get; }
        public long WinCount { get; }
        public double WinRatio { get; }
        public long WinsToDesiredPercent { get; }
        public Constants.Badge Badge { get; }

        public TankModel(string name, long battleCount, long winCount, Constants.Badge badge)
        {
            Name = name;
            BattleCount = battleCount;
            WinCount = winCount;
            WinRatio = Computer.GetWinRatio(battleCount, winCount);
            Badge = badge;
            WinsToDesiredPercent = WinRatio < Constants.DesiredWinPercent
                                   ? Computer.GetBattleCountToDesiredRatio(battleCount, winCount, Constants.DesiredWinPercent, Constants.MaxWinPercent)
                                   : 0;
        }
    }
}
