using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotStat
{
    class TankModel
    {
        private string name;
        private long battleCount;
        private double winRatio;
        private long winsToDesiredPercent;

        public string Name
        {
            get
            {
                return name;
            }
        }
        public long BattleCount
        {
            get
            {
                return battleCount;
            }
        }
        public double WinRatio
        {
            get
            {
                return winRatio;
            }
        }
        public long WinsToDesiredPercent
        {
            get
            {
                return winsToDesiredPercent;
            }
        }
        public TankModel(string name, long battleCount, double winRatio, long winsToDesiredPercent)
        {
            this.name = name;
            this.battleCount = battleCount;
            this.winRatio = winRatio;
            this.winsToDesiredPercent = winsToDesiredPercent;
        }
    }
}
