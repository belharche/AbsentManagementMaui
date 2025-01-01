using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AbsentManagement.Model
{

    [Table("students")]
    public class Student
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Firstame { get; set; }
        public string Lastame { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        // Foreign Key
        public int FieldId { get; set; }
    }
}
