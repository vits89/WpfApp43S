using System;
using System.Collections;
using System.Linq;
using System.Windows;
using WpfApp43S.Commands;
using WpfApp43S.Models;

namespace WpfApp43S.ViewModels
{
    public partial class MainWindowViewModel
    {
        private RelayCommand<Student> _addStudent;
        private RelayCommand<Student> _editStudent;
        private RelayCommand<ICollection> _deleteStudents;

        public RelayCommand<Student> AddStudent
        {
            get
            {
                if (_addStudent == null)
                {
                    _addStudent = new RelayCommand<Student>(student =>
                    {
                        if ((student == null) || student.HasErrors) return;

                        try
                        {
                            _repository.Add(student);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }, student => !((student == null) || student.HasErrors));
                }

                return _addStudent;
            }
        }

        public RelayCommand<Student> EditStudent
        {
            get
            {
                if (_editStudent == null)
                {
                    _editStudent = new RelayCommand<Student>(student =>
                    {
                        if ((student == null) || (student.Id < 0) || student.HasErrors) return;

                        try
                        {
                            _repository.Update(student);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }, student => !((student == null) || (student.Id < 0) || student.HasErrors));
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
                            var students = collection.Cast<Student>();

                            if (!students.Any()) return;

                            var text = string.Format("Вы действительно хотите удалить {0}?", students.Count() == 1 ? "выделенную запись" : "выделенные записи");
                            var result = MessageBox.Show(text, "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            if (result != MessageBoxResult.Yes) return;

                            _repository.Delete(students);
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
    }
}
