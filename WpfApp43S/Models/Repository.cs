using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace WpfApp43S.Models
{
    public class Repository : IRepository
    {
        private const string FILE_NAME = "Students.xml";

        private readonly XDocument _document = new XDocument(new XElement("Students"));
        private readonly ObservableCollection<Student> _students = new ObservableCollection<Student>();

        public Repository()
        {
            try
            {
                _document = XDocument.Load(FILE_NAME);

                foreach (var element in _document.Root.Elements())
                {
                    var student = new Student
                    {
                        Id = int.Parse(element.Attribute("Id").Value),
                        FirstName = element.Element("FirstName").Value,
                        LastName = element.Element("Last").Value,
                        Gender = int.Parse(element.Element("Gender").Value)
                    };

                    if (element.Element("Age") != null)
                    {
                        student.Age = int.Parse(element.Element("Age").Value);
                    }

                    _students.Add(student);
                }

                _document.Root.RemoveNodes();
            }
            catch
            {
            }
        }

        public void Add(Student student)
        {
            if (student == null) return;

            student.Id = _students.Any() ? _students.Max(s => s.Id) + 1 : 0;

            _students.Add(student);

            Save();
        }

        public ICollection<Student> Get() => _students;

        public void Update(Student student)
        {
            if (student == null) return;

            var existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);

            if (existingStudent == null) return;

            existingStudent.FirstName = student.FirstName;
            existingStudent.LastName = student.LastName;
            existingStudent.Gender = student.Gender;
            existingStudent.Age = student.Age;

            Save();
        }

        public void Delete(IEnumerable<Student> students)
        {
            if (!(students?.Any() ?? false)) return;

            var ids = students.Select(s => s.Id).ToArray();

            foreach (var id in ids)
            {
                _students.Remove(_students.First(s => s.Id == id));
            }

            for (var i = 0; i < _students.Count; i++)
            {
                _students[i].Id = i;
            }

            Save();
        }

        private void Save()
        {
            var elements = _students.Select(s =>
            {
                var element = new XElement(
                    "Student",
                    new XAttribute("Id", s.Id),

                    new XElement("FirstName", s.FirstName),
                    new XElement("Last", s.LastName)
                );

                if (s.Age.HasValue)
                {
                    element.Add(new XElement("Age", s.Age.Value));
                }

                element.Add(new XElement("Gender", s.Gender.Value));

                return element;
            });

            _document.Root.ReplaceNodes(elements);

            try
            {
                _document.Save(FILE_NAME);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
