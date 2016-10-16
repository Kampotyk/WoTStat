using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfSampleBasicChart
{
    /// <summary>
    ///     Implementation of <see cref="INotifyPropertyChanged" /> and Frameworkelement to simplify base drawing classes.
    /// </summary>
    public abstract class NotifierBase : INotifyPropertyChanged
    {
        /// <summary>
        ///     Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Checks if a property already matches a desired value.
        ///     Sets the property and notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.
        ///     This value is optional and can be provided automatically when invoked from compilers that
        ///     support CallerMemberName.
        /// </param>
        /// <returns>
        ///     True if the value was changed, false if the existing value matched the
        ///     desired value.
        /// </returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {

            if (Equals(storage, value))
            {
                return false;
            }

            storage = value;
            this.OnPropertyChanged<String>(propertyName);
            return true;
        }

        /// <summary>
        ///     Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of the property used to notify listeners.
        ///     This value is optional and can be provided automatically when invoked from compilers
        ///     that support <see cref="CallerMemberNameAttribute"/>.
        /// </param>
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            // make sure only to call this if the value actually changes
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
