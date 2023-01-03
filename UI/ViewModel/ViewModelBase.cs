using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UI.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected bool OnPropertyChanged<T>(ref T prop, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(prop, value)) return false;
            prop = value;
            this.RaisePropertyChange(propertyName);
            return true;
        }
        public void RaisePropertyChange(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
