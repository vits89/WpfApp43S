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
            field ??= new RelayCommand<StudentViewModel>(studentVm =>
            {
                if (studentVm is null || studentVm.HasErrors)
                {
                    return;
                }

                SelectedStudent = null;

                var student = _mapper.Map<Student>(studentVm);

                try
                {
                    _repository.Add(student);

                    studentVm.Id = student.Id;

                    Students.Add(studentVm);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, studentVm => !(studentVm is null || studentVm.HasErrors));

            return field;
        }
    }

    public IRelayCommand<StudentViewModel> EditStudent
    {
        get
        {
            field ??= new RelayCommand<StudentViewModel>(studentVm =>
            {
                if (studentVm is null || studentVm.Id < 0 || studentVm.HasErrors)
                {
                    return;
                }

                try
                {
                    _repository.Update(_mapper.Map<Student>(studentVm));

                    var existingStudentVm = Students.First(s => s.Id == studentVm.Id);

                    existingStudentVm.FirstName = studentVm.FirstName;
                    existingStudentVm.LastName = studentVm.LastName;
                    existingStudentVm.Gender = studentVm.Gender;
                    existingStudentVm.Age = studentVm.Age;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, studentVm => !(studentVm is null || studentVm.Id < 0 || studentVm.HasErrors));

            return field;
        }
    }

    public IRelayCommand<ICollection> DeleteStudents
    {
        get
        {
            field ??= new RelayCommand<ICollection>(collection =>
            {
                try
                {
                    var studentVms = collection!.Cast<StudentViewModel>();

                    if (!studentVms.Any())
                    {
                        return;
                    }

                    var text = string.Format(
                        "Вы действительно хотите удалить {0}?",
                        studentVms.Count() == 1 ? "выделенную запись" : "выделенные записи");

                    var result = MessageBox.Show(
                        text,
                        "Подтвердите удаление",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result != MessageBoxResult.Yes)
                    {
                        return;
                    }

                    _repository.Delete(_mapper.Map<IEnumerable<Student>>(studentVms));

                    var ids = studentVms.Select(s => s.Id).ToArray();

                    foreach (var id in ids)
                    {
                        Students.Remove(Students.First(s => s.Id == id));
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, collection => (collection?.Count ?? 0) > 0);

            return field;
        }
    }

    public IRelayCommand SetSelectedStudent
    {
        get
        {
            field ??= new RelayCommand(() =>
            {
                SelectedStudent ??= new StudentViewModel();
            });

            return field;
        }
    }
}
