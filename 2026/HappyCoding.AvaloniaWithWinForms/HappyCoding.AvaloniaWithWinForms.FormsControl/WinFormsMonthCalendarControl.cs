using System.ComponentModel;

namespace HappyCoding.AvaloniaWithWinForms.FormsControl;

public partial class WinFormsMonthCalendarControl : UserControl
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string TitleText
    {
        get => _lblTitle.Text;
        set => _lblTitle.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime SelectedStartDate
    {
        get => _ctrlMonthCalendar.SelectionStart;
        set => _ctrlMonthCalendar.SelectionStart = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime SelectedEndDate
    {
        get => _ctrlMonthCalendar.SelectionEnd;
        set => _ctrlMonthCalendar.SelectionEnd = value;
    }

    public event EventHandler<EventArgs>? SelectedDateChanged;

    public WinFormsMonthCalendarControl()
    {
        InitializeComponent();
    }

    private void OnCtrlMonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
    {
        SelectedDateChanged?.Invoke(this, EventArgs.Empty);
    }
}
