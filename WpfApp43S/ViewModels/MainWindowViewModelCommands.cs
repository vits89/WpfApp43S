using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel
    {
        private IRelayCommand<StudentViewModel>? _addStudent;
        private IRelayCommand<StudentViewModel>? _editStudent;
        private IRelayCommand<ICollection>? _deleteStudents;

        private IRelayCommand? _setSelectedStudent;

        public IRelayCommand<StudentViewModel> AddStudent
        {
            get
            {
                _addStudent ??= new RelayCommand<StudentViewModel>(studentVm =>
                {
                    if ((studentVm == null) || studentVm.HasErrors)
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
                }, studentVm => !((studentVm == null) || studentVm.HasErrors));

                return _addStudent;
            }
        }

        public IRelayCommand<StudentViewModel> EditStudent
        {
            get
            {
                _editStudent ??= new RelayCommand<StudentViewModel>(studentVm =>
                {
                    if ((studentVm == null) || (studentVm.Id < 0) || studentVm.HasErrors)
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
                }, studentVm => !((studentVm == null) || (studentVm.Id < 0) || studentVm.HasErrors));

                return _editStudent;
            }
        }

        public IRelayCommand<ICollection> DeleteStudents
        {
            get
            {
                _deleteStudents ??= new RelayCommand<ICollection>(collection =>
                {
                    try
                    {
                        var studentVms = collection!.Cast<StudentViewModel>();

                        if (!studentVms.Any())
                        {
                            return;
                        }

                        var text = string.Format("Вы действительно хотите удалить {0}?",
                            studentVms.Count() == 1 ? "выделенную запись" : "выделенные записи");

                        var result = MessageBox.Show(text, "Подтвердите удаление", MessageBoxButton.YesNo,
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

                return _deleteStudents;
            }
        }

        public IRelayCommand SetSelectedStudent
        {
            get
            {
                _setSelectedStudent ??= new RelayCommand(() =>
                {
                    SelectedStudent ??= new StudentViewModel();
                });

                return _setSelectedStudent;
            }
        }
    }
}
