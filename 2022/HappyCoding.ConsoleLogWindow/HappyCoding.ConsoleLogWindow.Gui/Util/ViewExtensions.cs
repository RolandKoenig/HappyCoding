using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Controls;
using HappyCoding.ConsoleLogWindow.Messenger;

namespace HappyCoding.ConsoleLogWindow.Gui.Util;

public static class ViewExtensions
{
    public static TViewModel? CreateAndAttachViewModel<TViewModel>(this Control control)
        where TViewModel : ViewModelBase
    {
        if (Design.IsDesignMode)
        {
            return null;
        }

        if (Avalonia.Application.Current is not App currentApp)
        {
            throw new InvalidOperationException("Application is not loaded correctly!");
        }

        var viewModel = currentApp.Services?.GetService<TViewModel>();
        if (viewModel == null)
        {
            throw new InvalidOperationException($"Unable to create view model of type {typeof(TViewModel).FullName}!");
        }

        var messageSubscriber = currentApp.Services?.GetService<IFirLibMessageSubscriber>();
        if (messageSubscriber == null)
        {
            throw new InvalidOperationException($"Unable to get message subscriber!");
        }

        var viewAdapter = new ViewAdapter(control);

        control.DataContext = viewModel;
        IEnumerable<MessageSubscription>? subscriptions = null;
        if (control is WindowBase targetWindow)
        {
            targetWindow.Activated += (_, _) =>
            {
                viewModel.ViewLoaded(viewAdapter);
                SubscribeAllMessages(ref subscriptions, messageSubscriber, viewModel);
            };
            targetWindow.Deactivated += (_, _) =>
            {
                UnsubscribeAllMessages(ref subscriptions);
                viewModel.ViewUnloaded();
            };
        }
        else
        {
            control.AttachedToLogicalTree += (_, _) =>
            {
                viewModel.ViewLoaded(viewAdapter);
                SubscribeAllMessages(ref subscriptions, messageSubscriber, viewModel);
            };
            control.DetachedFromLogicalTree += (_, _) =>
            {
                UnsubscribeAllMessages(ref subscriptions);
                viewModel.ViewUnloaded();
            };
        }
        

        return viewModel;
    }

    private static void SubscribeAllMessages(
        ref IEnumerable<MessageSubscription>? subscriptions,
        IFirLibMessageSubscriber messageSubscriber, object target)
    {
        UnsubscribeAllMessages(ref subscriptions);

        messageSubscriber.SubscribeAll(target);
    }

    private static void UnsubscribeAllMessages(ref IEnumerable<MessageSubscription>? subscriptions)
    {
        if (subscriptions == null) { return; }

        foreach (var actSubscription in subscriptions)
        {
            actSubscription.Dispose();
        }

        subscriptions = null;
    }
}