using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbsentManagement.Model
{
   
    public class Lesson
    {
        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }

        public string Title { get; set; }

        public int FilliereId {  get; set; }

    }
}
