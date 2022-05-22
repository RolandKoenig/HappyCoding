using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using HappyCoding.SimpleWinUI3App.Util;

namespace HappyCoding.SimpleWinUI3App.Pages;

public class ValidatingInputFormViewModel : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, string> _errors = new();

    private string _stringInput = "";

    public bool HasErrors => _errors.Count > 0;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public string StringInput
    {
        get => _stringInput;
        set
        {
            if (_stringInput != value)
            {
                _stringInput = value;
                RaisePropertyChanged();
            }

            this.CheckErrors();
        }
    }

    private void CheckErrors()
    {
        if (string.IsNullOrEmpty(StringInput))
        {
            _errors[nameof(StringInput)] = "StringInput is a required property!";
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(StringInput)));
        }
        else if(_errors.ContainsKey(nameof(StringInput)))
        {
            _errors.Remove(nameof(StringInput));
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(StringInput)));
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return Array.Empty<string>();
        }

        if (_errors.TryGetValue(propertyName, out var errorString))
        {
            return new string[] {errorString};
        }

        return Array.Empty<string>();
    }
}
