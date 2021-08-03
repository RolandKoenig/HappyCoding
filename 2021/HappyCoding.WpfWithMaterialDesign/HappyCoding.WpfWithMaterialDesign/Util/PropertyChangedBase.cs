using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HappyCoding.WpfWithMaterialDesign.Util
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}