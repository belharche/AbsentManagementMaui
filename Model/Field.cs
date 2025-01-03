using SQLite;

namespace AbsentManagement.Model
{
    [Table("filds")]
    public class Field
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
