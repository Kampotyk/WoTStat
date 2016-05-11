using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WotStat
{
    class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TankModel> tanks;

        public ViewModel()
        {
            Tanks = GetTanks();
        }

        public ObservableCollection<TankModel> Tanks
        {
            get { return tanks; }
            set
            {
                tanks = value;
                OnPropertyChanged("Tanks");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private ObservableCollection<TankModel> GetTanks()
        {
            return new ObservableCollection<TankModel>();
        }

    }
}
