namespace CalendarApp.Desktop.MyCalendar;

public class MyCalendarDay
{
    
    public MyCalendar Calendar=>Year.Calendar;
    public MyCalendarYear Year=>Month.Year;
    public readonly MyCalendarMonth Month;
    public readonly int Day;
    public readonly DayOfWeek DayOfWeek;
    
    public readonly LinkedList<MyCalendarRecord> Records=new();
    
    public MyCalendarDay(MyCalendarMonth month, int day)
    {
        Month = month;
        Day = day;
        DayOfWeek = new DateOnly(Year.Year, (int)month.Month, Day).DayOfWeek;
    }
    
    
    public void AddRecord(MyCalendarRecord record)
    {
        var lastRecord = Records.Last;
        var beforeLast = lastRecord?.Previous;
            
        while(true)
        {
            if (lastRecord == null || beforeLast==null)
            {
                Records.AddFirst(record);
                return;
            }

            if (beforeLast.Value.DateAndTime < record.DateAndTime &&
                record.DateAndTime < lastRecord.Value.DateAndTime)
                Records.AddBefore(lastRecord, record);

            lastRecord = beforeLast;
            beforeLast = beforeLast.Previous;
        }
    }

    public void DeleteRecord(MyCalendarRecord record)
    {
        if (!Records.Remove(record))
            throw new Exception($"can't remove record: {record}");
    }
}