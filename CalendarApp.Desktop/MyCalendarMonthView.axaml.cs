using Avalonia.Controls;
using Avalonia.Interactivity;
using CalendarApp.Desktop.MyCalendar;

namespace CalendarApp.Desktop;

public partial class MyCalendarMonthView : UserControl
{
    
    private const string calendarFileName = "calendarRecords.dtsod";
    public MyCalendarLoader CalendarLoader;
    public MyCalendar.MyCalendar Calendar => CalendarLoader.LoadedCalendar;
    public MyCalendarMonth CurrentMonth;
    
    public MyCalendarMonthView()
    {
        InitializeComponent();
        CalendarLoader = new MyCalendarLoader(calendarFileName);
        var now = DateTime.Now;
        // CalendarLoader.AddRecord(new MyCalendarRecord(now, "some event", null));
        // CalendarLoader.AddRecord(new MyCalendarRecord(now.Add(TimeSpan.FromHours(1.5)), "next event", "nyaa"));
        // CalendarLoader.Save();
        YearBox.TextInput += (s, e) =>
        {
            try
            {
                var year=Calendar[e.Text.ToInt()];
                ShowMonth(year[CurrentMonth!.Month]);
            }
            catch {}
        };
        MonthSelector.SelectionChanged += (s, e) =>
        {
            MonthEnum mn = (MonthEnum)(e.AddedItems[0] ?? throw new Exception("selected month is null"));
            ShowMonth(CurrentMonth!.Year[mn]);
        };
        CurrentMonth = Calendar[now.Year][now.Month];
        ShowMonth(CurrentMonth);
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
    
    public void ShowMonth(MyCalendarMonth month)
    {
        CurrentMonth = month;
        int totalDaysAdded=0;
        DaysGrid.Children.Clear();
        MonthSelector.SelectedIndex = (int)month.Month - 1;
        YearBox.Text = month.Year.Year.ToString();

        // days before current month
        MyCalendarMonth prevMonth = CurrentMonth.PreviousMonth();
        int prevMonthDaysN = GetDayColumnN(month[1].DayOfWeek);
        for(int i=prevMonth.Days.Length-prevMonthDaysN; i<prevMonth.Days.Length; i++)
        {
            var dayView = new MyCalendarDayView(prevMonth[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7);
            Grid.SetRow(dayView, totalDaysAdded/7);
            DaysGrid.Children.Add(dayView);
            totalDaysAdded++;
        }
        
        // days of current month
        for(int i=0; i< month.Days.Length; i++)
        {
            var dayView = new MyCalendarDayView(month[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7);
            Grid.SetRow(dayView, totalDaysAdded/7);
            DaysGrid.Children.Add(dayView);
            totalDaysAdded++;
        }
        
        // days after current month
        MyCalendarMonth nextMonth = month.NextMonth();
        int nextMonthDaysN = 6-GetDayColumnN(month[month.Days.Length].DayOfWeek);
        for(int i=0; i<nextMonthDaysN; i++)
        {
            var dayView = new MyCalendarDayView(nextMonth[i + 1]);
            Grid.SetColumn(dayView, totalDaysAdded%7);
            Grid.SetRow(dayView, totalDaysAdded/7);
            DaysGrid.Children.Add(dayView);
            totalDaysAdded++;
        }
    }

    private void PreviousMonth_pressed(object? sender, RoutedEventArgs e) => ShowMonth(CurrentMonth.PreviousMonth());

    private void NextMonth_pressed(object? sender, RoutedEventArgs e) => ShowMonth(CurrentMonth.NextMonth());
}