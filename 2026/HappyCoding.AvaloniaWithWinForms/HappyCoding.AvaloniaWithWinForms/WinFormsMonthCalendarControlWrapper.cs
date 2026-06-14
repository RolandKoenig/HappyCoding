using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using HappyCoding.AvaloniaWithWinForms.FormsControl;
using System;

namespace HappyCoding.AvaloniaWithWinForms;

public class WinFormsMonthCalendarControlWrapper : NativeControlHost
{
    public static readonly DirectProperty<WinFormsMonthCalendarControlWrapper, string> TitleTextProperty =
        AvaloniaProperty.RegisterDirect<WinFormsMonthCalendarControlWrapper, string>(
            nameof(TitleText),
            o => o.TitleText,
            (o, v) => o.TitleText = v);

    public static readonly DirectProperty<WinFormsMonthCalendarControlWrapper, DateTimeOffset> SelectedStartDateProperty =
        AvaloniaProperty.RegisterDirect<WinFormsMonthCalendarControlWrapper, DateTimeOffset>(
            nameof(SelectedStartDate),
            o => o.SelectedStartDate,
            (o, v) => o.SelectedStartDate = v);

    public static readonly DirectProperty<WinFormsMonthCalendarControlWrapper, DateTimeOffset> SelectedEndDateProperty =
        AvaloniaProperty.RegisterDirect<WinFormsMonthCalendarControlWrapper, DateTimeOffset>(
            nameof(SelectedEndDate),
            o => o.SelectedEndDate,
            (o, v) => o.SelectedEndDate = v);

    private WinFormsMonthCalendarControl? _userControl;

    private string _titleText = "Date Selection";
    private DateTimeOffset _selectedStartDate = DateTimeOffset.Now;
    private DateTimeOffset _selectedEndDate = DateTimeOffset.Now;

    public string TitleText
    {
        get => _titleText;
        set
        {
            SetAndRaise(TitleTextProperty, ref _titleText, value);
            if (_userControl != null)
            {
                _userControl.TitleText = value;
            }
        }
    }

    public DateTimeOffset SelectedStartDate
    {
        get => _selectedStartDate;
        set
        {
            SetAndRaise(SelectedStartDateProperty, ref _selectedStartDate, value);
            if (_userControl != null)
            {
                _userControl.SelectedStartDate = value.LocalDateTime;
            }
        }
    }

    public DateTimeOffset SelectedEndDate
    {
        get => _selectedEndDate;
        set
        {
            SetAndRaise(SelectedEndDateProperty, ref _selectedEndDate, value);
            if (_userControl != null)
            {
                _userControl.SelectedEndDate = value.LocalDateTime;
            }
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        _userControl = new WinFormsMonthCalendarControl();
        _userControl.CreateControl();

        if (_titleText != null)
        {
            _userControl.TitleText = _titleText;
            _userControl.SelectedStartDate = _selectedStartDate.LocalDateTime;
            _userControl.SelectedEndDate = _selectedEndDate.LocalDateTime;
            _userControl.SelectedDateChanged += OnUserControl_SelectedDateChanged;

        }

        return new PlatformHandle(_userControl.Handle, "HWND");
    }

    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        if (_userControl != null)
        {
            _userControl.Dispose();
            _userControl = null;
        }
    }

    private void OnUserControl_SelectedDateChanged(object? sender, System.EventArgs e)
    {
        if (_userControl == null) { return; }

        SetAndRaise(SelectedStartDateProperty, ref _selectedStartDate, new DateTimeOffset(_userControl.SelectedStartDate));
        SetAndRaise(SelectedEndDateProperty, ref _selectedEndDate, new DateTimeOffset(_userControl.SelectedEndDate));
    }
}
