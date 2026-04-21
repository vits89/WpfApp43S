using System.Collections.ObjectModel;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels;

public partial class MainWindowViewModel(IRepository repository, IMapper mapper) : ObservableObject
{
    public IReadOnlyDictionary<string, int?> GenderOptions { get; } = new Dictionary<string, int?>
    {
        { "", null },
        { "мужчина", 0 },
        { "женщина", 1 }
    }
    .AsReadOnly();

    public ObservableCollection<StudentViewModel> Students { get; } =
        new(mapper.Map<IEnumerable<StudentViewModel>>(repository.GetAll()));

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
}
