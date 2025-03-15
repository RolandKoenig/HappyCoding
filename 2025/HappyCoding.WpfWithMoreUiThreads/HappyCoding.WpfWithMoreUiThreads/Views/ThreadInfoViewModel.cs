using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HappyCoding.WpfWithMoreUiThreads.Views;

public partial class ThreadInfoViewModel : ObservableObject
{
    public int ThreadId => Thread.CurrentThread.ManagedThreadId;

    [ObservableProperty]
    private bool _isWorking;

    [RelayCommand]
    public void DoSomeSyncWork()
    {
        if (this.IsWorking) { return; }
        try
        {
            this.IsWorking = true;

            Thread.Sleep(5000);
        }
        finally
        {
            this.IsWorking = false;
        }
    }

    [RelayCommand]
    public async Task DoSomeAsyncWorkAsync()
    {
        if (this.IsWorking) { return; }
        try
        {
            this.IsWorking = true;

            await Task.Delay(5000);
        }
        finally
        {
            this.IsWorking = false;
        }
    }
}