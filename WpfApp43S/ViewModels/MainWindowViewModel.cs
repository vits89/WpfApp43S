using System.Collections.Generic;
using System.ComponentModel;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IRepository _repository;

        private Student _selectedStudent;

        public ICollection<Student> Students { get; }

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = (Student)value?.Clone();

                NotifyPropertyChanged(nameof(SelectedStudent));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(IRepository repository)
        {
            _repository = repository;

            Students = _repository.Get();
        }

        private void NotifyPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
