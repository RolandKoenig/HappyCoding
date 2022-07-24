using FakeItEasy;
using HappyCoding.ConsoleLogWindow.Application.Ports;
using HappyCoding.ConsoleLogWindow.Application.UseCases;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public static class DesignData
{
    public static ProcessGroupsViewModel ProcessGroupsViewModel
    {
        get
        {
            return new ProcessGroupsViewModel(
                A.Fake<IProcessGroupRepository>(),
                A.Fake<IProcessRunner>(),
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
                new StartProcessUseCase(
                    A.Fake<IProcessRunner>(),
                    A.Fake<IFirLibMessagePublisher>()));
        }
    }
}
