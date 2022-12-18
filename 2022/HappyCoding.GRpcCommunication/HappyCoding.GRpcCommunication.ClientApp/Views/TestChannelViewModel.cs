using System.Collections.ObjectModel;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels;
using RolandK.Patterns;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

public class TestChannelViewModel : PropertyChangedBase
{
    public ObservableCollection<TestChannelItemViewModel> TestChannels { get; }

    public TestChannelViewModel()
    {
        this.TestChannels = new ObservableCollection<TestChannelItemViewModel>();
        this.TestChannels.Add(new TestChannelItemViewModel(new PlainHttpChannel()));
        this.TestChannels.Add(new TestChannelItemViewModel(new SimpleRequestReplyWithStringChannel()));
    }
}
