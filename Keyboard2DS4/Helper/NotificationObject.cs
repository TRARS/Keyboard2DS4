using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Keyboard2DS4.Helper
{
    public class NotificationObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
