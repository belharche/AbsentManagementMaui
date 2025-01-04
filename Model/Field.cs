using SQLite;

namespace AbsentManagement.Model
{
    [Table("filds")]
    public class Field
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public Field()
        {
            Id = 0;
            Name = "";

        }
        public Field(int Id , string Name)
        {
            this.Id = Id;
            this.Name = Name;   
        }
    }
}
