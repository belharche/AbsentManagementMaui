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

   
    public class Student
    {
        [PrimaryKey]
        public string Id { get; set; } // l'id de l'élève represente le cne ici 
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        
        public int FieldId { get; set; }    
    }
}
