namespace CalendarApp.Desktop.MyCalendar;

public class MyCalendarLoader
{
    public MyCalendar LoadedCalendar=new MyCalendar();
    public string DtsodFileName;
    private DtsodV23 storageDtsod;
    
    public MyCalendarLoader(string _dtsodFileName)
    {
        DtsodFileName = _dtsodFileName;
        
        if (File.Exists(DtsodFileName))
        {
            storageDtsod = new DtsodV23(File.ReadAllText(DtsodFileName));
            foreach (var recordDtsod in storageDtsod["records"])
                LoadedCalendar.AddRecord(new MyCalendarRecord((DtsodV23)recordDtsod));
        }
        else storageDtsod = new DtsodV23() { {"records", new List<dynamic>() } };
    }
    
    
    public void AddRecord(MyCalendarRecord record)
    {
        LoadedCalendar.AddRecord(record);
        storageDtsod["records"].Add(record.ToDtsod());
    }

    public void DeleteRecord(MyCalendarRecord record)
    {
        LoadedCalendar.DeleteRecord(record);
        storageDtsod["records"].Remove(record.ToDtsod());
    }

    public void Save()
    {
        File.WriteAllText(DtsodFileName, storageDtsod.ToString());
    }
}