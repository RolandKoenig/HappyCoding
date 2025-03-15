using System.Windows;

namespace HappyCoding.WpfWithMoreUiThreads;

public partial class WindowWithCustomThread : Window
{
    public WindowWithCustomThread()
    {
        InitializeComponent();
    }

    public static void ShowInNewUihread()
    {
        Thread newWindowThread = new Thread(ThreadStartingPoint);
        newWindowThread.SetApartmentState(ApartmentState.STA);
        newWindowThread.IsBackground = true;
        newWindowThread.Start();
    }

    private static void ThreadStartingPoint()
    {
        new WindowWithCustomThread().Show();

        System.Windows.Threading.Dispatcher.Run();
    }
}