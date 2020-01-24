using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace WpfApp43S.Models
{
    public class Repository : IRepository
    {
        private const string FILE_NAME = "Students.xml";
        private const string ROOT_ELEMENT_NAME = "Students";

        private readonly ObservableCollection<Student> _students = new ObservableCollection<Student>();

        public Repository()
        {
            try
            {
                var serializer = new XmlSerializer(_students.GetType(), new XmlRootAttribute(ROOT_ELEMENT_NAME));

                using (var stream = new FileStream(FILE_NAME, FileMode.Open))
                {
                    _students = (ObservableCollection<Student>)serializer.Deserialize(stream);
                }
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
            var serializer = new XmlSerializer(_students.GetType(), new XmlRootAttribute(ROOT_ELEMENT_NAME));

            using (var stream = new FileStream(FILE_NAME, FileMode.Create))
            {
                serializer.Serialize(stream, _students);
            }
        }
    }
}
