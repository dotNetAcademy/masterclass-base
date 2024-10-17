namespace TimesheetApp.Domain.Models;

public class Holiday
{
    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }

    public Holiday(DateTime date, string name)
    {
        Date = date;
        Name = name;
    }
}
