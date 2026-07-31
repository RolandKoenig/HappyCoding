using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace HappyCoding.AvaloniaGlobalExceptionHandling;

public partial class MainWindowViewModel : ObservableObject
{
    [RelayCommand]
    private void ThrowExceptionInSynchronousCode()
    {
        throw new DemoException($"Demo exception from {nameof(ThrowExceptionInSynchronousCode)}");
    }
    
    [RelayCommand]
    private async Task ThrowExceptionInAsynchronousCodeAsync()
    {
        await Task.Delay(500);
        
        throw new DemoException($"Demo exception from {nameof(ThrowExceptionInAsynchronousCodeAsync)}");
    }
    
    [RelayCommand]
    private async void ThrowExceptionInAsyncVoidMethodAsync()
    {
        await Task.Delay(500);
        
        throw new DemoException($"Demo exception from {nameof(ThrowExceptionInAsyncVoidMethodAsync)}");
    }
    
    [RelayCommand]
    private void ThrowExceptionInBackgroundTask()
    {
        Task.Run(() =>
        {
            throw new DemoException($"Demo exception from {nameof(ThrowExceptionInBackgroundTask)}");
        });
    }
    
    [RelayCommand]
    private void ThrowExceptionInThreadPool()
    {
        ThreadPool.QueueUserWorkItem(_ =>
        {
            throw new DemoException($"Demo exception from {nameof(ThrowExceptionInBackgroundTask)}");
        });
    }
}