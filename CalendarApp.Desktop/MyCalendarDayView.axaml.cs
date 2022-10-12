using Avalonia;
using Avalonia.Controls;
using CalendarApp.Desktop.MyCalendar;

namespace CalendarApp.Desktop;

public partial class MyCalendarDayView : UserControl
{
    public MyCalendarDay? Day;
    
    public MyCalendarDayView() => InitializeComponent();

    public MyCalendarDayView(MyCalendarDay day) : this()
    {
        Day = day;
        textBlock.Text = day.Day.ToString();
    }

    
}