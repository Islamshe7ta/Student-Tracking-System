using StudentTrackingSystem.BLL.Repositories;
using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IStudentRepository : IGenaricRepository<Student>
    {
        Task<int> GetCountAsync();
        Task<List<Student>> GetRecentAsync(int count);
    }
}
        // ممكن تضيف دوال خاصة بالطالب هنا لو هتحتاجها

        //IEnumerable<Student> GetAll();
        //Student GetById(int id);
        //void Add(Student student);
        //void Update(Student student);
        //void Delete(int id);