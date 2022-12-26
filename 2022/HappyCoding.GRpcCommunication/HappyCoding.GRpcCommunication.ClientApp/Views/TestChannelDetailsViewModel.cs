using RolandK.Patterns;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

internal class TestChannelDetailsViewModel : PropertyChangedBase
{
    private TestChannelItemViewModel? _currentItem;

    public TestChannelItemViewModel? CurrentItem
    {
        get => _currentItem;
        set
        {
            base.SetProperty(ref _currentItem, value);

            base.RaisePropertyChanged(nameof(this.Command_Start));
            base.RaisePropertyChanged(nameof(this.Command_Stop));
            base.RaisePropertyChanged(nameof(this.Command_ResetMetrics));
        }
    }

    public DelegateCommand? Command_Start => _currentItem?.Command_Start;

    public DelegateCommand? Command_Stop => _currentItem?.Command_Stop;

    public DelegateCommand? Command_ResetMetrics => _currentItem?.Command_ResetMetrics;
}
