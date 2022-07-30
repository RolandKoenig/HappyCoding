using FakeItEasy;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.Services.DocumentModelHandling;
using HappyCoding.ConsoleLogWindow.Application.Services.UseCaseExecution;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

internal static class DesignData
{
    public static MainWindowViewModel MainWindowViewModel
    {
        get
        {
            return new MainWindowViewModel(
                A.Fake<IDocumentModelProvider>(),
                A.Fake<IUseCaseExecutor>());
        }
    }

    public static ProcessGroupsViewModel ProcessGroupsViewModel
    {
        get
        {
            return new ProcessGroupsViewModel(
                A.Fake<IDocumentModelProvider>(),
                A.Fake<IFirLibMessagePublisher>());
        }
    }

    public static RunningProcessViewModel RunningProcessViewModel
    {
        get
        {
            return new RunningProcessViewModel(
                A.Fake<IProcessRunner>(),
                A.Fake<IFirLibMessageSubscriber>(),
                A.Fake<IUseCaseExecutor>());
        }
    }
}
