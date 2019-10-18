namespace WotStat.Extensions
{
    static class TankExtensions
    {
        public static TankModel Create(string name, long battleCount, long winCount, Constants.Badge badge)
        {
            TankModel tank = new TankModel {
                Name = name,
                BattleCount = battleCount,
                WinCount = winCount,
                WinRatio = Computer.GetWinRatio(battleCount, winCount),
                Badge = badge
            };

            if (tank.WinRatio < Constants.DesiredWinPercent)
            {
                tank.WinsToDesiredPercent = Computer.GetBattleCountToDesiredRatio(battleCount
                    , winCount
                    , Constants.DesiredWinPercent
                    , Constants.MaxWinPercent);
            }

            return tank;
        }
    }
}
