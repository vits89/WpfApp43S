using System.Collections.Generic;

namespace WpfApp43S.Models
{
    public interface IRepository
    {
        void Add(Student student);
        Student? Get(int id);
        IEnumerable<Student> GetAll();
        void Update(Student student);
        void Delete(IEnumerable<Student> students);
    }
}
