using Avalonia.Controls;
using CalendarApp.Desktop.MyCalendar;

namespace CalendarApp.Desktop;

public partial class MyCalendarMonthView : UserControl
{
    
    private const string calendarFileName = "calendarRecords.dtsod";
    public MyCalendarLoader CalendarLoader;
    public MyCalendar.MyCalendar Calendar => CalendarLoader.LoadedCalendar;
    
    public MyCalendarMonthView()
    {
        InitializeComponent();
        CalendarLoader = new MyCalendarLoader(calendarFileName);
        var now = DateTime.Now;
        CalendarLoader.AddRecord(new MyCalendarRecord(now, "some event", null));
        CalendarLoader.AddRecord(new MyCalendarRecord(now.Add(TimeSpan.FromHours(1.5)), "next event", "nyaa"));
        CalendarLoader.Save();
        ShowMonth(now.Year, now.Month);
    }

    int GetDayColumnN(DayOfWeek d) => d switch
    {
        DayOfWeek.Monday => 0,
        DayOfWeek.Tuesday => 1,
        DayOfWeek.Wednesday => 2,
        DayOfWeek.Thursday => 3,
        DayOfWeek.Friday => 4,
        DayOfWeek.Saturday => 5,
        DayOfWeek.Sunday =>6,
        _ => throw new ArgumentOutOfRangeException(nameof(d), d, null)
    };
    
    public void ShowMonth(int yearN, int monthN)
    {
        MyCalendarMonth month = Calendar[yearN][monthN];
        int totalDaysAdded=0;
        
        // days before current month
        MyCalendarMonth prevMonth;
        if (monthN == 1)
            prevMonth = Calendar[yearN - 1][12];
        else prevMonth = Calendar[yearN][monthN-1];
        int prevMonthDaysN = GetDayColumnN(month[1].DayOfWeek);
        for(int i=prevMonth.Days.Length-prevMonthDaysN; i<prevMonth.Days.Length; i++)
        {
            var dayView = new MyCalendarDayView(prevMonth[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7 +1);
            Grid.SetRow(dayView, totalDaysAdded/7 +1);
            grid.Children.Add(dayView);
            totalDaysAdded++;
        }
        
        // days of current month
        for(int i=0; i< month.Days.Length; i++)
        {
            var dayView = new MyCalendarDayView(month[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7 +1);
            Grid.SetRow(dayView, totalDaysAdded/7 +1);
            grid.Children.Add(dayView);
            totalDaysAdded++;
        }
        
        // days after current month
        MyCalendarMonth nextMonth;
        if (monthN == 12)
            nextMonth = Calendar[yearN + 1][1];
        else nextMonth = Calendar[yearN][monthN + 1];
        int nextMonthDaysN = 6-GetDayColumnN(month[month.Days.Length].DayOfWeek);
        for(int i=0; i<nextMonthDaysN; i++)
        {
            var dayView = new MyCalendarDayView(nextMonth[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7 +1);
            Grid.SetRow(dayView, totalDaysAdded/7 +1);
            grid.Children.Add(dayView);
            totalDaysAdded++;
        }
        
    }
}