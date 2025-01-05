using SQLite;

public class Absence
{
    [PrimaryKey, AutoIncrement]
    public int AbsenceID { get; set; }
    public string StudentID { get; set; }
    public string LessonName { get; set; }
    public DateTime Date { get; set; } 
    public bool IsJustified { get; set; }
}
