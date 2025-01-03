using SQLite;

public class Absence
{
    [PrimaryKey, AutoIncrement]
    public int AbsenceID { get; set; }
    public int StudentID { get; set; }
    public int LessonID { get; set; }
    public DateTime Date { get; set; }  // Date the absence was recorded
    public bool IsJustified { get; set; }
}
