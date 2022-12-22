using System.Collections.ObjectModel;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;
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

        this.AddTestChannel(new Http1ChannelSimpleRequest());
        this.AddTestChannel(new Http1ChannelComplexRequest());
        this.AddTestChannel(new Http1ChannelComplexRequestParallel());

        this.AddTestChannel(new Http2ChannelSimpleRequest());
        this.AddTestChannel(new Http2ChannelComplexRequest());
        this.AddTestChannel(new Http2ChannelComplexRequestParallel());

        this.AddTestChannel(new GRpcChannelSimpleRequest());
        this.AddTestChannel(new GRpcChannelComplexRequest());
        this.AddTestChannel(new GRpcChannelComplexRequestParallel());
    }

    private void AddTestChannel(ITestChannel channel)
    {
        this.TestChannels.Add(new TestChannelItemViewModel(channel));

    }
}
