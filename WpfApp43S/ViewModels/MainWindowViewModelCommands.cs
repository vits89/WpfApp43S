using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfApp43S.Commands;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel
    {
        private RelayCommand<StudentViewModel> _addStudent;
        private RelayCommand<StudentViewModel> _editStudent;
        private RelayCommand<ICollection> _deleteStudents;

        private RelayCommand<object> _setSelectedStudent;

        public RelayCommand<StudentViewModel> AddStudent
        {
            get
            {
                if (_addStudent == null)
                {
                    _addStudent = new RelayCommand<StudentViewModel>(studentVm =>
                    {
                        if ((studentVm == null) || studentVm.HasErrors) return;

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
                }

                return _addStudent;
            }
        }

        public RelayCommand<StudentViewModel> EditStudent
        {
            get
            {
                if (_editStudent == null)
                {
                    _editStudent = new RelayCommand<StudentViewModel>(studentVm =>
                    {
                        if ((studentVm == null) || (studentVm.Id < 0) || studentVm.HasErrors) return;

                        var student = _mapper.Map<Student>(studentVm);

                        try
                        {
                            _repository.Update(student);

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
                }

                return _editStudent;
            }
        }

        public RelayCommand<ICollection> DeleteStudents
        {
            get
            {
                if (_deleteStudents == null)
                {
                    _deleteStudents = new RelayCommand<ICollection>(collection =>
                    {
                        try
                        {
                            var studentVms = collection.Cast<StudentViewModel>();

                            if (!studentVms.Any()) return;

                            var text = string.Format("Вы действительно хотите удалить {0}?", studentVms.Count() == 1 ?
                                "выделенную запись" : "выделенные записи");
                            var result = MessageBox.Show(text, "Подтвердите удаление", MessageBoxButton.YesNo,
                                MessageBoxImage.Question);

                            if (result != MessageBoxResult.Yes) return;

                            var students = _mapper.Map<IEnumerable<Student>>(studentVms);

                            _repository.Delete(students);

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
                }

                return _deleteStudents;
            }
        }

        public RelayCommand<object> SetSelectedStudent
        {
            get
            {
                if (_setSelectedStudent == null)
                {
                    _setSelectedStudent = new RelayCommand<object>(parameter =>
                    {
                        SelectedStudent ??= new StudentViewModel();
                    });
                }

                return _setSelectedStudent;
            }
        }
    }
}
