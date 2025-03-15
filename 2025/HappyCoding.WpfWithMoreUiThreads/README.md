# More UI Threads in WPF
 - WPF allows us to create nore UI threads
 - You can run a new Dispatcher loop in a separate thread using System.Windows.Threading.Dispatcher.Run();
 - Windows and Controls created on this Thread belong to this Thread

## Recommendation
 - Don't use that. The normal way should be to put heavy processing load to background threads