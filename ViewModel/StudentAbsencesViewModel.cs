using AbsentManagement.Database;
using AbsentManagement.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Reactive.Linq;

namespace AbsentManagement.ViewModel
{
    [QueryProperty(nameof(Stud), "Student")]
    public partial class StudentAbsencesViewModel : ObservableObject
    {
        [ObservableProperty]
        private Student stud;

        [ObservableProperty]
        private ObservableCollection<Absence> allAbsence;

        public DbService DbService { get; set; }

        public StudentAbsencesViewModel(DbService dbService)
        {
            DbService = dbService;
            allAbsence = new ObservableCollection<Absence>();
            System.Diagnostics.Debug.WriteLine("const lancer");
        }

        // Fixed method name: 'OnStudChanged' instead of 'OnStudtChanged'
        partial void OnStudChanged(Student value)
        {
            if (value != null)
            {
                LoadAbsence();
            }
        }

        public async void LoadAbsence()
        {
            try
            {
                if (stud == null)
                {
                    System.Diagnostics.Debug.WriteLine("Student is null");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Loading absences for student: " + stud.Id);
                var absencesFromDb = await DbService.GetAbsenceByUserId(stud.Id);
                Console.WriteLine(absencesFromDb);
                AllAbsence = new ObservableCollection<Absence>(absencesFromDb);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading absences: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task RemoveAbsence(Absence absence)
        {
            try
            {
                await DbService.DeleteAbsence(absence);
                AllAbsence.Remove(absence);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing absence: {ex.Message}");
            }
        }
    }
}
