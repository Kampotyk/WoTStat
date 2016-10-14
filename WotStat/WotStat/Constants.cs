using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotStat
{
    static class Constants
    {
        public const int DesiredWinPercent = 50;
        public const string AccountListUrl = "https://api.worldoftanks.ru/wot/account/list/";
        public const string TanksListUrl = "https://api.worldoftanks.ru/wot/encyclopedia/vehicles/";
        public const string PlayersTanksUrl = "https://api.worldoftanks.ru/wot/account/tanks/";
    }
}
