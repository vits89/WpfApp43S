using System.Collections.Generic;

namespace WpfApp43S.Models
{
    public interface IRepository
    {
        void Add(Student student);
        ICollection<Student> Get();
        void Update(Student student);
        void Delete(IEnumerable<Student> students);
    }
}
