using System;
using System.Collections.Generic;
using Avalonia.Controls.ApplicationLifetimes;
using Caliburn.Micro;

namespace HappyCoding.AvaloniaWithCaliburnMicro;

public class Bootstrapper : BootstrapperBase
{
    private SimpleContainer _container = new SimpleContainer();

    public Bootstrapper()
    {
        Initialize();
    }

    protected override void Configure()
    {
        _container.Instance(_container);
        _container
            .Singleton<IWindowManager, WindowManager>()
            .Singleton<IEventAggregator, EventAggregator>();
        
        _container
            .PerRequest<ShellViewModel>();
    }

    protected override async void OnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        await DisplayRootViewFor<ShellViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }
}