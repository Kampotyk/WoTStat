using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using WotStat.Models;
using WotStatService.Models;

namespace WotStat
{
    internal class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TankModel> tanks;
        private TankModel selectedTank;
        private Region defaultRegion = new Region { Name = "Russia", UrlSuffix = "ru" };

        public ObservableCollection<TankModel> Tanks
        {
            get => tanks;
            set
            {
                tanks = value;
                OnPropertyChanged("Tanks");
            }
        }

        public TankModel SelectedTank
        {
            get => selectedTank;
            set
            {
                if (selectedTank != value)
                {
                    selectedTank = value;
                    OnPropertyChanged("SelectedTank");
                }
            }
        }

        public TankModel PrevSelectedTank { get; set;}

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task<bool> LoadPlayerStats(string playerName)
        {
            Tanks = await Task.Factory.StartNew(() => StatService.GetPlayersTanks(
                StatService.GetAccountIdByName(playerName, defaultRegion)
                , StatService.GetAllTanks(defaultRegion)
                , defaultRegion));
            return true;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
