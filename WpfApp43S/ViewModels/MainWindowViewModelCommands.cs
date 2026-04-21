using System.Collections;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels;

public partial class MainWindowViewModel
{
    public IRelayCommand<StudentViewModel> AddStudent
    {
        get
        {
            field ??= new RelayCommand<StudentViewModel>(
                studentVm =>
                {
                    SelectedStudent = null;

                    var student = mapper.Map<Student>(studentVm);

                    try
                    {
                        repository.Add(student);

                        studentVm!.Id = student.Id;

                        Students.Add(studentVm);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                },
                studentVm => studentVm is not null && !studentVm.HasErrors);

            return field;
        }
    }

    public IRelayCommand<StudentViewModel> EditStudent
    {
        get
        {
            field ??= new RelayCommand<StudentViewModel>(
                studentVm =>
                {
                    try
                    {
                        repository.Update(mapper.Map<Student>(studentVm));

                        var existingStudentVm = Students.First(s => s.Id == studentVm!.Id);

                        mapper.Map(studentVm, existingStudentVm);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                },
                studentVm => studentVm is not null && studentVm.Id >= 0 && !studentVm.HasErrors);

            return field;
        }
    }

    public IRelayCommand<IReadOnlyCollection<StudentViewModel>> DeleteStudents
    {
        get
        {
            field ??= new RelayCommand<IReadOnlyCollection<StudentViewModel>>(
                studentVms =>
                {
                    var text = string.Format(
                        "Вы действительно хотите удалить {0}?",
                        studentVms!.Count == 1 ? "выделенную запись" : "выделенные записи");

                    var result = MessageBox.Show(
                        text,
                        "Подтвердите удаление",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    try
                    {
                        repository.Delete(mapper.Map<IEnumerable<Student>>(studentVms));

                        foreach (var studentVm in studentVms)
                        {
                            Students.Remove(studentVm);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                },
                studentVms => (studentVms?.Count ?? 0) > 0);

            return field;
        }
    }

    public IRelayCommand SetSelectedStudent
    {
        get
        {
            field ??= new RelayCommand(() => SelectedStudent ??= new StudentViewModel());

            return field;
        }
    }

    public IRelayCommand<IList> SetSelectedStudents
    {
        get
        {
            field ??= new RelayCommand<IList>(
                items => SelectedStudents = items?.Cast<StudentViewModel>().ToList().AsReadOnly());

            return field;
        }
    }
}
