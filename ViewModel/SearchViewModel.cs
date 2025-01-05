using System.Collections.ObjectModel;
using AbsentManagement.Model;
using AbsentManagement.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AbsentManagement.ViewModel
{
    public partial class SearchViewModel : ObservableObject
    {
        private readonly DbService _dbService;
        private ObservableCollection<Student> _allStudents;

        [ObservableProperty]
        private ObservableCollection<Student> students;

        public SearchViewModel(DbService dbService)
        {
            _dbService = dbService;
            _allStudents = new ObservableCollection<Student>();
            Students = new ObservableCollection<Student>();
            LoadStudentsAsync();
        }

        private async void LoadStudentsAsync()
        {
            try
            {
                var studentsList = await _dbService.GetStudents();
                _allStudents = new ObservableCollection<Student>(studentsList);
                Students = new ObservableCollection<Student>(_allStudents);
            }
            catch (Exception ex)
            {
                // Handle any errors, perhaps show an alert to the user
                await Shell.Current.DisplayAlert("Error", "Failed to load students.", "OK");
            }
        }

        [RelayCommand]
        private void Search(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Students = new ObservableCollection<Student>(_allStudents);
                return;
            }

            var filteredStudents = _allStudents.Where(s =>
                s.Firstname.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                s.Lastname.Contains(searchText, StringComparison.OrdinalIgnoreCase));
              

            Students = new ObservableCollection<Student>(filteredStudents);
        }

        [RelayCommand]
        private async Task ViewAbsences(Student student)
        {
            System.Diagnostics.Debug.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"+student.Id);

            if (student == null)
                return;

            var parameters = new Dictionary<string, object>
            {
                { "Student", student }
            };

            await Shell.Current.GoToAsync("StudentAbsencesView", parameters);
        }
    }
}