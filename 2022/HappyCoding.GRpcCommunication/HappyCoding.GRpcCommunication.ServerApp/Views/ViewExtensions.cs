
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

internal static class ViewExtensions
{
    public static void RegisterViewModelMessageHandler(this StyledElement view)
    {
        IEnumerable<MessageSubscription>? messageSubscriptions = null;

        if (view is Window window)
        {
            window.Activated += (_, _) =>
            {
                if (messageSubscriptions != null) { return; }
                if (view.DataContext == null) { return; }

                var viewModel = view.DataContext;
                var messenger = FirLibMessenger.GetByName(ViewConstants.VIEW_MESSENGER_NAME);
                messageSubscriptions = messenger.SubscribeAll(viewModel);
            };
            window.Deactivated += (_, _) =>
            {
                if (messageSubscriptions == null) { return; }

                foreach (var actSubscription in messageSubscriptions)
                {
                    actSubscription.Dispose();
                }
                messageSubscriptions = null;
            };
        }
        else
        {
            view.AttachedToLogicalTree += (_, _) =>
            {
                if (messageSubscriptions != null) { return; }
                if (view.DataContext == null) { return; }

                var viewModel = view.DataContext;
                var messenger = FirLibMessenger.GetByName(ViewConstants.VIEW_MESSENGER_NAME);
                messageSubscriptions = messenger.SubscribeAll(viewModel);
            };
            view.DetachedFromLogicalTree += (_, _) =>
            {
                if (messageSubscriptions == null) { return; }

                foreach (var actSubscription in messageSubscriptions)
                {
                    actSubscription.Dispose();
                }
                messageSubscriptions = null;
            };
        }
    }
}
