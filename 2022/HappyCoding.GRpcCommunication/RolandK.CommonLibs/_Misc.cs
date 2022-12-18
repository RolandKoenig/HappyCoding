namespace RolandK;

public enum ActionIfSyncContextIsNull
{
    InvokeSynchronous,

    InvokeUsingNewTask,

    DontInvoke
}