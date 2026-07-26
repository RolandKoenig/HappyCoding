using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using System;
using System.Windows.Forms;

namespace HappyCoding.AvaloniaWithWinForms;

public class WinFormsMonthCalendar : NativeControlHost
{
    public static readonly DirectProperty<WinFormsMonthCalendar, DateTime> SelectionStartProperty =
        AvaloniaProperty.RegisterDirect<WinFormsMonthCalendar, DateTime>(
            nameof(SelectionStart),
            o => o.SelectionStart,
            (o, v) => o.SelectionStart = v);

    public static readonly DirectProperty<WinFormsMonthCalendar, DateTime> SelectionEndProperty =
        AvaloniaProperty.RegisterDirect<WinFormsMonthCalendar, DateTime>(
            nameof(SelectionEnd),
            o => o.SelectionEnd,
            (o, v) => o.SelectionEnd = v);

    private MonthCalendar? _userControl;
    private DateTime _selectionStart = DateTime.Now;
    private DateTime _selectionEnd = DateTime.Now;

    public DateTime SelectionStart
    {
        get => _selectionStart;
        set
        {
            SetAndRaise(SelectionStartProperty, ref _selectionStart, value);
            if (_userControl != null)
            {
                _userControl.SelectionStart = value;
            }
        }
    }

    public DateTime SelectionEnd
    {
        get => _selectionEnd;
        set
        {
            SetAndRaise(SelectionEndProperty, ref _selectionEnd, value);
            if (_userControl != null)
            {
                _userControl.SelectionEnd = value;
            }
        }
    }

    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        _userControl = new MonthCalendar();
        _userControl.CreateControl();

        _userControl.SelectionStart = _selectionStart;
        _userControl.SelectionEnd = _selectionEnd;
        _userControl.DateChanged += OnUserControl_SelectedDateChanged;

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

        SetAndRaise(SelectionStartProperty, ref _selectionStart, _userControl.SelectionStart);
        SetAndRaise(SelectionEndProperty, ref _selectionEnd, _userControl.SelectionEnd);
    }
}
