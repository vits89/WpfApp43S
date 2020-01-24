using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        private StudentViewModel _selectedStudent;

        public ObservableCollection<StudentViewModel> Students { get; }

        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = (StudentViewModel)value?.Clone();

                NotifyPropertyChanged();
            }
        }

        public MainWindowViewModel(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            Students = new ObservableCollection<StudentViewModel>(_mapper
                .Map<IEnumerable<StudentViewModel>>(_repository.GetAll()));
        }
    }
}
