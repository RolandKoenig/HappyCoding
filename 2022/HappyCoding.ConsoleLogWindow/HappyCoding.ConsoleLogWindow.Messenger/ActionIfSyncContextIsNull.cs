namespace HappyCoding.ConsoleLogWindow.Messenger;

internal enum ActionIfSyncContextIsNull
{
    InvokeSynchronous,

    InvokeUsingNewTask,

    DontInvoke
}