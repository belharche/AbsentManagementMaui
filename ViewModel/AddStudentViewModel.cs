using AbsentManagement.Database;
using AbsentManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AbsentManagement.ViewModel
{
    public class AddStudentViewModel : INotifyPropertyChanged
    {
        private readonly DbService _dbService;

        private string _id;
        private string _firstname;
        private string _lastname;
        private string _email;
        private string _phone;
        private string _gender;
        private bool _isErrorVisible;
        private string _errorMsg;
        private List<Field> _fields;
        private Field _selectedField;

        public ICommand AddStudentCommand { get; }

        public string Id { 
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }
        public string Firstame { 
            get => _firstname;
            set { _firstname = value; OnPropertyChanged(); }
        }
        public string Lastame { 
            get => _lastname;
            set { _lastname = value; OnPropertyChanged(); } 
        }
        public string Email { 
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        public string Phone { 
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }
        public List<Field> Fields { 
            get => _fields;
            set { _fields = value; OnPropertyChanged(); }
        }
        public Field SelectedField { 
            get => _selectedField;
            set { _selectedField = value; OnPropertyChanged(); } 
        }
        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(); }
        }
        public string ErrorMsg
        {
            get => _errorMsg;
            set { _errorMsg = value; OnPropertyChanged(); }
        }
        public bool IsErrorVisible { 
            get => _isErrorVisible;
            set { _isErrorVisible = value; OnPropertyChanged(); } 
        }

        public AddStudentViewModel(DbService dbService) { 
            _dbService = dbService;
            AddStudentCommand = new Command(async () => await HandleAddStudent());
            LoadFields();
        }



        private async Task LoadFields()
        {
            try
            {
                Fields = await _dbService.GetFields();
            }
            catch (Exception ex)
            {
                ErrorMsg = "Field To Load Fields";
            }
        }


        private async Task HandleAddStudent()
        {
            // Data validation
            if (string.IsNullOrEmpty(Firstame) || string.IsNullOrEmpty(Lastame) || string.IsNullOrEmpty(Email) || SelectedField == null)
            {
                ErrorMsg = "Please fill in all required fields.";
                return;
            }

            // Create new Student
            var student = new Student
            {
                Firstame = Firstame,
                Lastame = Lastame,
                Email = Email,
                Phone = Phone,
                Gender = Gender,
                FieldId = SelectedField.Id
            };

            try
            {
                // Insert student into database
                await _dbService.InsertStudent(student);
                ErrorMsg = "Student added successfully!";
            }
            catch (Exception ex)
            {
                ErrorMsg = $"Error: {ex.Message}";
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
