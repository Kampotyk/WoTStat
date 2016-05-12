using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
            var accountId = GetAccountIdByName("Dr_John");
            var tanks = GetAllTanks();
            return new ObservableCollection<TankModel>();
        }

        private static string GetAccountIdByName(string name)
        {
            var accountId = String.Empty;
            var requestParams = new NameValueCollection();

            requestParams.Add("application_id", "12845e99af9d4a7b3c734c0cbbb5ee12");
            requestParams.Add("search", name);
            requestParams.Add("limit", "1");

            var jsonResult = Request.PostRequest(Constants.accountListUrl, requestParams);
            if(!String.IsNullOrEmpty(jsonResult))
            {
                dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                accountId = result.data.Last.account_id;
            }

            return accountId;
        }

        private static Dictionary<string, string> GetAllTanks()
        {
            var tanks = new Dictionary<string, string>();

            var requestParams = new NameValueCollection();
            requestParams.Add("application_id", "12845e99af9d4a7b3c734c0cbbb5ee12");
            requestParams.Add("language", "en");
            requestParams.Add("fields", "short_name_i18n, tank_id");

            var jsonResult = Request.PostRequest(Constants.tanksListUrl, requestParams);
            if (!String.IsNullOrEmpty(jsonResult))
            {
                dynamic result = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                foreach(dynamic tank in result.data)
                {
                    dynamic value = tank.Value;
                    tanks.Add(value.tank_id.ToString(), value.short_name_i18n.ToString());
                }
            }

            return tanks;
        }

    }
}
