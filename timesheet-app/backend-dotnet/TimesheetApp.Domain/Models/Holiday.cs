namespace TimesheetApp.Domain.Models;

public class Holiday
{
    public Holiday(DateTime date, string name)
    {
        Date = date;
        Name = name;
    }

    public long Id { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }
}
