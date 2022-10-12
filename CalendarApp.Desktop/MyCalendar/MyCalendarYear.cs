namespace CalendarApp.Desktop.MyCalendar;

public class MyCalendarYear
{
    public int Year;
    public bool IsLeapYear;
    
    private MyCalendarMonth[] Months;
    
    public MyCalendarMonth this[int month]
    {
        get
        {
            if (month < 1 || month > 12)
                throw new Exception($"incorrect month number: {month}");
            return Months[month - 1];
        }
    }

    public MyCalendarMonth this[MyCalendarMonth.MonthEnum month] => Months[(int)month - 1];

    public MyCalendarYear(int year)
    {
        Year = year;
        IsLeapYear = DateTime.IsLeapYear(year);
        Months = new MyCalendarMonth[12];
        for(int m=0; m<12; m++)
        {
            Months[m] = new MyCalendarMonth(this, m + 1);
        }
    }
}