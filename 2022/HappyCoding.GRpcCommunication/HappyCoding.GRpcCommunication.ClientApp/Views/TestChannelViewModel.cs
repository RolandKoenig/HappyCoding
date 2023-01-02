using System.Collections.ObjectModel;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;
using HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;
using RolandK.Patterns;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ClientApp.Views;

public class TestChannelViewModel : PropertyChangedBase
{
    private TestChannelItemViewModel? _selectedTestChannel;

    public ObservableCollection<TestChannelItemViewModel> TestChannels { get; }

    public DelegateCommand Command_ResetMetrics { get; }

    public TestChannelItemViewModel? SelectedTestChannel
    {
        get => _selectedTestChannel;
        set => this.SetProperty(ref _selectedTestChannel, value);
    }

    public TestChannelViewModel()
    {
        this.TestChannels = new ObservableCollection<TestChannelItemViewModel>();

        this.Command_ResetMetrics = new DelegateCommand(this.ResetMetrics);

        this.AddTestChannel(new Http1ChannelSimpleRequest());
        this.AddTestChannel(new Http1ChannelComplexRequest());
        this.AddTestChannel(new Http1ChannelComplexRequestParallel());

        this.AddTestChannel(new Http2ChannelSimpleRequest());
        this.AddTestChannel(new Http2ChannelComplexRequest());
        this.AddTestChannel(new Http2ChannelComplexRequestParallel());

        this.AddTestChannel(new GrpcChannelSimpleRequest());
        this.AddTestChannel(new GrpcChannelSimpleRequestStreamed());
        this.AddTestChannel(new GrpcChannelComplexRequest());
        this.AddTestChannel(new GrpcChannelComplexRequestParallel());
    }

    private void AddTestChannel(ITestChannel channel)
    {
        this.TestChannels.Add(new TestChannelItemViewModel(channel));
    }

    private void ResetMetrics()
    {
        foreach (var actTestChannelVm in this.TestChannels)
        {
            actTestChannelVm.ResetMetrics();
        }
    }
}
