using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HappyCoding.SimpleWinUI3App.Util;

namespace HappyCoding.SimpleWinUI3App.Pages;

public class InputFormViewModel : ViewModelBase
{
    private string _stringInput = "ABC";
    private int _intInput = 50;
    private DateTimeOffset _dateTime = DateTimeOffset.Now;
    private TimeSpan _timeSpan = TimeSpan.FromMinutes(5.0);

    [Required]
    public string StringInput
    {
        get => _stringInput;
        set
        {
            if (_stringInput != value)
            {
                _stringInput = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(AllContent));
            }
        }
    }

    public int IntInput
    {
        get => _intInput;
        set
        {
            if (_intInput != value)
            {
                _intInput = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(AllContent));
            }
        }
    }

    public DateTimeOffset DateTimeInput
    {
        get => _dateTime;
        set
        {
            if (_dateTime != value)
            {
                _dateTime = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(AllContent));
            }
        }
    }

    public TimeSpan TimeInput
    {
        get => _timeSpan;
        set
        {
            if (_timeSpan != value)
            {
                _timeSpan = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(AllContent));
            }
        }
    }

    public string AllContent
    {
        get
        {
            var resultBuilder = new StringBuilder();
            
            resultBuilder.Append("String = ");
            resultBuilder.Append(this.StringInput);

            resultBuilder.Append(Environment.NewLine);
            resultBuilder.Append("Int = ");
            resultBuilder.Append(this.IntInput);

            resultBuilder.Append(Environment.NewLine);
            resultBuilder.Append("DateTimeOffset = ");
            resultBuilder.Append(this.DateTimeInput.ToString("yyyy-MM-dd"));

            resultBuilder.Append(Environment.NewLine);
            resultBuilder.Append("Time = ");
            resultBuilder.Append(this.TimeInput.ToString());

            return resultBuilder.ToString();
        }
        set{ }
    }

    public override void OnHostLoaded()
    {
        base.OnHostLoaded();
    }

    public override void OnHostUnloaded()
    {
        base.OnHostUnloaded();
    }
}
