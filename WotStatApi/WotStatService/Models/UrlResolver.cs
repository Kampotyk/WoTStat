namespace WotStatService.Models
{
    public static class UrlResolver
    {
        private const string _openIdLoginUrl = "https://api.worldoftanks.{0}/wot/auth/login/";
        private const string _accountInfo = "https://api.worldoftanks.{0}/wot/account/info/";
        private const string _accountListUrl = "https://api.worldoftanks.{0}/wot/account/list/";
        private const string _tanksListUrl = "https://api.worldoftanks.{0}/wot/encyclopedia/vehicles/";
        private const string _playersTanksUrl = "https://api.worldoftanks.{0}/wot/account/tanks/";
        private const string _tanksMasteryListUrl = "https://mastery.poliroid.{0}/api/{0}/vehicles";

        public static string OpenIdLogin(Region region)
        {
            return string.Format(_openIdLoginUrl, region.UrlSuffix);
        }
        public static string AccountInfo(Region region)
        {
            return string.Format(_accountInfo, region.UrlSuffix);
        }
        public static string AccountListUrl(Region region)
        {
            return string.Format(_accountListUrl, region.UrlSuffix);
        }
        public static string TanksListUrl(Region region)
        {
            return string.Format(_tanksListUrl, region.UrlSuffix);
        }
        public static string PlayersTanksUrl(Region region)
        {
            return string.Format(_playersTanksUrl, region.UrlSuffix);
        }
        public static string TanksMasteryListUrl(Region region)
        {
            return string.Format(_tanksMasteryListUrl, region.UrlSuffix);
        }
    }
}
