namespace WotStat
{
    internal static class Constants
    {
        public enum Badge
        {
            None = 0,
            Third = 1,
            Second = 2,
            First = 3,
            Master = 4
        }

        public const double DesiredWinPercent = 50.0;
        public const double MaxWinPercent = 100.0;
        public const double GraphStep = 5.0;

        public const string AccountListUrl = "https://api.worldoftanks.ru/wot/account/list/";
        public const string TanksListUrl = "https://api.worldoftanks.ru/wot/encyclopedia/vehicles/";
        public const string PlayersTanksUrl = "https://api.worldoftanks.ru/wot/account/tanks/";
    }
}
