using FakeItEasy;
using HappyCoding.ConsoleLogWindow.Domain.Ports;

namespace HappyCoding.ConsoleLogWindow.Gui.Views;

public static class DesignData
{
    public static ProcessGroupsViewModel ProcessGroupsViewModel
    {
        get
        {
            return new ProcessGroupsViewModel(
                A.Fake<IProcessGroupRepository>(),
                A.Fake<IProcessRunner>());
        }
    }
}
