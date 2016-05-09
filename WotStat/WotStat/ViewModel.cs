using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WotStat
{
    class ViewModel : INotifyPropertyChanged
    {
        private TankModel m_model;

        public ViewModel()
        {
            m_model = new TankModel();
        }

        public string Name
        {
            get { return m_model.name; }
            set
            {
                if (m_model.name != value)
                {
                    m_model.name = value;
                    InvokePropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChangedEventHandler changed = PropertyChanged;
            if (changed != null) changed(this, e);
        }
    }
}
