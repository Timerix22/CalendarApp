namespace CalendarApp.Desktop.MyCalendar;

public class MyCalendar
{
    Dictionary<int, MyCalendarYear> Years= new();
    
    public MyCalendarYear this[int yearNum]
    {
        get
        {
            if (!Years.TryGetValue(yearNum, out var year))
            {
                year = new MyCalendarYear(yearNum);
                Years.Add(yearNum, year);
            }

            return year;
        }
    }
    
    internal void AddRecord(MyCalendarRecord record)
    {
        var day = this[record.DateAndTime.Year][record.DateAndTime.Month][record.DateAndTime.Day];
        day.AddRecord(record);
    }

    internal void DeleteRecord(MyCalendarRecord record)
    {
        var day = this[record.DateAndTime.Year][record.DateAndTime.Month][record.DateAndTime.Day];
        day.DeleteRecord(record);
    }
}