using System.IO;
using System.Xml.Serialization;

namespace WpfApp43S.Models;

public class XmlFileRepository : IRepository
{
    private const string fileName = "Students.xml";
    private const string rootElementName = "Students";

    private readonly ICollection<Student> _students = new List<Student>();

    public XmlFileRepository()
    {
        try
        {
            var serializer = new XmlSerializer(_students.GetType(), new XmlRootAttribute(rootElementName));

            using var stream = new FileStream(fileName, FileMode.Open);

            _students = (ICollection<Student>)serializer.Deserialize(stream)!;
        }
        catch
        {
        }
    }

    public void Add(Student student)
    {
        student.Id = _students.Count > 0 ? _students.Max(s => s.Id) + 1 : 0;

        _students.Add(student);

        Save();
    }

    public Student? Get(int id) => _students.FirstOrDefault(s => s.Id == id);

    public IEnumerable<Student> GetAll() => _students;

    public void Update(Student student)
    {
        var existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);

        if (existingStudent is null)
        {
            return;
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Gender = student.Gender;
        existingStudent.Age = student.Age;

        Save();
    }

    public void Delete(IEnumerable<Student> students)
    {
        var ids = students.Select(s => s.Id).ToArray();

        foreach (var id in ids)
        {
            _students.Remove(_students.First(s => s.Id == id));
        }

        Save();
    }

    private void Save()
    {
        var serializer = new XmlSerializer(_students.GetType(), new XmlRootAttribute(rootElementName));

        using var stream = new FileStream(fileName, FileMode.Create);

        serializer.Serialize(stream, _students);
    }
}
