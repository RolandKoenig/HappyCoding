using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HappyCoding.WpfWithMoreUiThreads.Views;

public partial class ThreadInfoViewModel : ObservableObject
{
    public int ThreadId => Thread.CurrentThread.ManagedThreadId;

    [RelayCommand]
    public async Task DoSomeAsyncWorkAsync()
    {
        await Task.Delay(5000);
    }
}