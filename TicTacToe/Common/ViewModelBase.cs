using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TicTacToe
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Dictionary<string, object> _props = new Dictionary<string, object>();
        protected void Set<T>(T newValue, [CallerMemberName] string property = null)
        {
            if (property == null)
                return;
            if(_props.TryGetValue(property, out var oldValue) && oldValue is T o && o.Equals(newValue))
                return;
            _props[property] = newValue;
            FirePropertyChanged(property);
        }
        
        protected T Get<T>([CallerMemberName] string property = null) 
        {
            if (property != null)
            {
                if (_props.TryGetValue(property, out var oldValue) && oldValue is T v)
                    return v;
            }
            return default(T);
        }

        protected void FirePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
