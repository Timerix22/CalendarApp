namespace CalendarApp.Desktop.MyCalendar;

public record struct MyCalendarRecord(DateTime DateAndTime, string Name, string? Description)
{
    public DtsodV23 ToDtsod()
    {
        var dtsod = new DtsodV23();
        dtsod.Add("dateTime", DateAndTime.ToString(MyTimeFormat.ForText));
        dtsod.Add("name", Name);
        if(Description is not null)
            dtsod.Add("description", Description);
        return dtsod;
    }

    public MyCalendarRecord(DtsodV23 dtsod) : this(
        DateTime.Parse((string)dtsod["dateTime"]),
        (string)dtsod["name"],
        dtsod.TryGetValue("description", out var desc) ? (string)desc : null) 
    { }
}