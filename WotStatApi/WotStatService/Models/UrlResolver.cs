﻿namespace WotStatService.Models
{
    public static class UrlResolver
    {
        private const string _openIdLoginUrl = "https://api.worldoftanks.{0}/wot/auth/login/";
        private const string _logoutUrl = "https://api.worldoftanks.{0}/wot/auth/logout/";
        private const string _accountInfo = "https://api.worldoftanks.{0}/wot/account/info/";
        private const string _accountListUrl = "https://api.worldoftanks.{0}/wot/account/list/";
        private const string _tanksListUrl = "https://api.worldoftanks.{0}/wot/encyclopedia/vehicles/";
        private const string _playersTanksUrl = "https://api.worldoftanks.{0}/wot/account/tanks/";
        private const string _playerTanksAchievementsUrl = "https://api.worldoftanks.{0}/wot/tanks/achievements/";
        private const string _tanksMasteryListUrl = "https://poliroid.me/mastery/api/{0}/vehicles/";
        private const string _tanksGunMarksListUrl = "https://poliroid.me/gunmarks/api/{0}/vehicles/";

        public static string OpenIdLogin(Region region)
        {
            return string.Format(_openIdLoginUrl, region.UrlSuffix);
        }
        public static string LogoutUrl(Region region)
        {
            return string.Format(_logoutUrl, region.UrlSuffix);
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
        public static string PlayerTanksAchievementsUrl(Region region)
        {
            return string.Format(_playerTanksAchievementsUrl, region.UrlSuffix);
        }
        public static string TanksMasteryListUrl(Region region)
        {
            return string.Format(_tanksMasteryListUrl, region.UrlSuffix);
        }
        public static string TanksGunMarksListUrl(Region region)
        {
            return string.Format(_tanksGunMarksListUrl, region.UrlSuffix);
        }
    }
}
