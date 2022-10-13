namespace CalendarApp.Desktop.MyCalendar;

public class MyCalendarMonth
{
    public MyCalendar Calendar => Year.Calendar;
    public readonly MyCalendarYear Year;
    public readonly MonthEnum Month;
    public readonly MyCalendarDay[] Days;
    
    public MyCalendarMonth(MyCalendarYear year,int month) : this(year, (MonthEnum)month) {}
    
    public MyCalendarMonth(MyCalendarYear year,MonthEnum month)
    {
        Year = year;
        Month = month;
        int daysCount = month switch
        {
            MonthEnum.January => 31,
            MonthEnum.February => year.IsLeapYear ? 29 : 28,
            MonthEnum.March => 31,
            MonthEnum.April => 30,
            MonthEnum.May => 31,
            MonthEnum.June => 30,
            MonthEnum.July => 31,
            MonthEnum.August => 31,
            MonthEnum.September => 30,
            MonthEnum.October => 31,
            MonthEnum.November => 30,
            MonthEnum.December => 31,
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };
        Days = new MyCalendarDay[daysCount];
        for (int d = 0; d < daysCount; d++)
            Days[d] = new MyCalendarDay(this, d+1);
    }

    public MyCalendarDay this[int dayNum]
    {
        get
        {
            if (Days.Length < dayNum)
                throw new Exception($"can't get day {dayNum}, because there are only {Days.Length} days in {Month}");
            return Days[dayNum - 1];
        }
    }

    public MyCalendarMonth PreviousMonth()
    {
        MyCalendarMonth prevMonth;
        if (Month == MonthEnum.January)
            prevMonth = Calendar[Year.Year - 1][12];
        else prevMonth = Calendar[Year.Year][(int)Month-1];
        return prevMonth;
    }

    public MyCalendarMonth NextMonth()
    {
        MyCalendarMonth nextMonth;
        if (Month==MonthEnum.December)
            nextMonth = Calendar[Year.Year + 1][1];
        else nextMonth = Calendar[Year.Year][(int)Month + 1];
        return nextMonth;
    }
}