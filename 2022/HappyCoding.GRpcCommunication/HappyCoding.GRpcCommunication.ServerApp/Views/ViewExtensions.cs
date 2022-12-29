using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using RolandK.Patterns.Messaging;
using RolandK.Patterns.Mvvm;

namespace HappyCoding.GRpcCommunication.ServerApp.Views;

internal static class ViewExtensions
{
    public static void RegisterViewModel(this StyledElement view)
    {
        IEnumerable<MessageSubscription>? messageSubscriptions = null;
        object? catchedViewModel = null;

        // Define register and deregister methods
        void RegisterAction()
        {
            if (messageSubscriptions != null) { return; }
            if (view.DataContext == null) { return; }

            var viewModel = view.DataContext;
            var messenger = FirLibMessenger.GetByName(ViewConstants.VIEW_MESSENGER_NAME);
            messageSubscriptions = messenger.SubscribeAll(viewModel);

            if (viewModel is ViewModelBase viewModelBase)
            {
                viewModelBase.AssociatedView = view;
            }

            catchedViewModel = viewModel;
        }
        void DeregisterAction()
        {
            if (messageSubscriptions == null) { return; }

            var viewModel = view.DataContext;
            foreach (var actSubscription in messageSubscriptions) { actSubscription.Dispose(); }
            messageSubscriptions = null;

            if (viewModel is ViewModelBase viewModelBase)
            {
                viewModelBase.AssociatedView = view;
            }

            catchedViewModel = null;
        }
        void ContextChangedAction()
        {
            var oldViewModel = catchedViewModel;
            var newViewModel = view.DataContext;

            if (messageSubscriptions != null)
            {
                foreach (var actSubscription in messageSubscriptions) { actSubscription.Dispose(); }
                messageSubscriptions = null;
            }
            if (oldViewModel is ViewModelBase oldViewModelBase)
            {
                oldViewModelBase.AssociatedView = null;
            }

            if (newViewModel != null)
            {
                var messenger = FirLibMessenger.GetByName(ViewConstants.VIEW_MESSENGER_NAME);
                messageSubscriptions = messenger.SubscribeAll(newViewModel);
            }

            if (newViewModel is ViewModelBase viewModelBase)
            {
                viewModelBase.AssociatedView = view;
            }

            catchedViewModel = newViewModel;
        }

        // Trigger register and deregister depending on view type
        if (view is Window window)
        {
            window.Activated += (_, _) =>
            {
                RegisterAction();
            };
            window.Deactivated += (_, _) =>
            {
                DeregisterAction();
            };
        }
        else
        {
            view.AttachedToLogicalTree += (_, _) =>
            {
                RegisterAction();
            };
            view.DetachedFromLogicalTree += (_, _) =>
            {
                DeregisterAction();
            };
        }

        view.DataContextChanged += (_, _) => ContextChangedAction();
    }
}
