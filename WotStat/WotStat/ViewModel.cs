﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using WotStat.Models;
using WotStatService.Models;

namespace WotStat
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TankModel> weakTanks;
        private TankModel selectedWeakTank;

        private ObservableCollection<TankModel> noMasterTanks;
        private TankModel selectedNoMasterTank;

        private Region defaultRegion = new Region { Name = "Russia", UrlSuffix = "ru" };

        public ObservableCollection<TankModel> WeakTanks
        {
            get => weakTanks;
            set
            {
                weakTanks = value;
                OnPropertyChanged("WeakTanks");
            }
        }

        public TankModel SelectedWeakTank
        {
            get => selectedWeakTank;
            set
            {
                if (selectedWeakTank != value)
                {
                    selectedWeakTank = value;
                    OnPropertyChanged("SelectedWeakTank");
                }
            }
        }

        public ObservableCollection<TankModel> NoMasterTanks
        {
            get => noMasterTanks;
            set
            {
                noMasterTanks = value;
                OnPropertyChanged("NoMasterTanks");
            }
        }

        public TankModel SelectedNoMasterTank
        {
            get => selectedNoMasterTank;
            set
            {
                if (selectedNoMasterTank != value)
                {
                    selectedNoMasterTank = value;
                    OnPropertyChanged("SelectedNoMasterTank");
                }
            }
        }

        public TankModel PrevSelectedWeakTank { get; set;}

        public TankModel PrevSelectedNoMasterTank { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task<bool> LoadPlayerTankStats(string playerName)
        {
            var allTanks = await Task.Factory.StartNew(() => StatService.GetPlayerTankStats(
                StatService.GetAccountIdByName(playerName, defaultRegion)
                , StatService.GetAllTanks(defaultRegion)
                , defaultRegion));

            WeakTanks = new ObservableCollection<TankModel>(allTanks.Where(tank => tank.WinsToDesiredPercent > 0));
            NoMasterTanks = new ObservableCollection<TankModel>(allTanks.Where(tank => tank.Badge != Constants.Badge.Master));

            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
