namespace WotStat
{
    class TankModel
    {
        private string name;
        private long battleCount;
        private long winCount;
        private double winRatio;
        private long winsToDesiredPercent;
        private Constants.Badge badge;

        public string Name
        {
            get { return name; }
        }
        public long BattleCount
        {
            get { return battleCount; }
        }
        public long WinCount
        {
            get { return winCount; }
        }
        public double WinRatio
        {
            get { return winRatio; }
        }
        public long WinsToDesiredPercent
        {
            get { return winsToDesiredPercent; }
        }
        public Constants.Badge Badge
        {
            get { return badge; }
        }

        public TankModel(string name, long battleCount, long winCount, Constants.Badge badge)
        {
            this.name = name;
            this.battleCount = battleCount;
            this.winCount = winCount;
            this.winRatio = Computer.GetWinRatio(battleCount, winCount);
            this.badge = badge;
            this.winsToDesiredPercent = winRatio < Constants.DesiredWinPercent
                                        ? Computer.GetBattleCountToDesiredRatio(battleCount, winCount, Constants.DesiredWinPercent, Constants.MaxWinPercent)
                                        : 0;
        }
    }
}
