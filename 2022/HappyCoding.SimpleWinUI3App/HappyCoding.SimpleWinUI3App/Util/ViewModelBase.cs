﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HappyCoding.SimpleWinUI3App.Util;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}