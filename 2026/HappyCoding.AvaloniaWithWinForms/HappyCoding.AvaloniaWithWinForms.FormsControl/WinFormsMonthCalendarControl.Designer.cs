namespace HappyCoding.AvaloniaWithWinForms.FormsControl;

partial class WinFormsMonthCalendarControl
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _ctrlMonthCalendar = new MonthCalendar();
        panel1 = new Panel();
        _lblTitle = new Label();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // _ctrlMonthCalendar
        // 
        _ctrlMonthCalendar.Dock = DockStyle.Fill;
        _ctrlMonthCalendar.Location = new Point(0, 41);
        _ctrlMonthCalendar.Name = "_ctrlMonthCalendar";
        _ctrlMonthCalendar.ShowWeekNumbers = true;
        _ctrlMonthCalendar.TabIndex = 1;
        _ctrlMonthCalendar.DateSelected += OnCtrlMonthCalendar_DateSelected;
        // 
        // panel1
        // 
        panel1.Controls.Add(_lblTitle);
        panel1.Dock = DockStyle.Top;
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(500, 41);
        panel1.TabIndex = 2;
        // 
        // _lblTitle
        // 
        _lblTitle.AutoSize = true;
        _lblTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        _lblTitle.Location = new Point(12, 12);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(62, 20);
        _lblTitle.TabIndex = 0;
        _lblTitle.Text = "<Title>";
        // 
        // WinFormsMonthCalendarControl
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_ctrlMonthCalendar);
        Controls.Add(panel1);
        Name = "WinFormsMonthCalendarControl";
        Size = new Size(500, 311);
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private MonthCalendar _ctrlMonthCalendar;
    private Panel panel1;
    private Label _lblTitle;
}
