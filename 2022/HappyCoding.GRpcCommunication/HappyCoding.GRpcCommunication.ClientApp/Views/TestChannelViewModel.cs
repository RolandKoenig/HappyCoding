using System.Collections.ObjectModel;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels;
using RolandK.Patterns;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

public class TestChannelViewModel : PropertyChangedBase
{
    private TestChannelItemViewModel? _selectedTestChannel;

    public ObservableCollection<TestChannelItemViewModel> TestChannels { get; }

    public TestChannelItemViewModel? SelectedTestChannel
    {
        get => _selectedTestChannel;
        set => this.SetProperty(ref _selectedTestChannel, value);
    }

    public TestChannelViewModel()
    {
        this.TestChannels = new ObservableCollection<TestChannelItemViewModel>();
        this.TestChannels.Add(new TestChannelItemViewModel(new PlainHttp2Channel()));
        this.TestChannels.Add(new TestChannelItemViewModel(new SimpleRequestReplyWithStringChannel()));
    }
}
