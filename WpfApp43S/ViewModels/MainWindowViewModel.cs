using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public IDictionary<string, int?> GenderOptions { get; } = new Dictionary<string, int?>
    {
        { "", null },
        { "мужчина", 0 },
        { "женщина", 1 }
    };

    public ObservableCollection<StudentViewModel> Students { get; }

    public StudentViewModel? SelectedStudent
    {
        get => field;
        set
        {
            field = (StudentViewModel?)value?.Clone();

            field?.ErrorsChanged += (_, _) =>
            {
                AddStudent.NotifyCanExecuteChanged();
                EditStudent.NotifyCanExecuteChanged();
            };

            OnPropertyChanged();
        }
    }

    public IReadOnlyCollection<StudentViewModel>? SelectedStudents
    {
        get => field;
        set
        {
            field = value;

            OnPropertyChanged();
        }
    }

    public MainWindowViewModel(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

        Students = new ObservableCollection<StudentViewModel>(
            _mapper.Map<IEnumerable<StudentViewModel>>(_repository.GetAll()));
    }
}
