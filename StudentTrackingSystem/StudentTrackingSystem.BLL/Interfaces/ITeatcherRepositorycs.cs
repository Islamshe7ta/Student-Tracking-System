﻿using StudentTrackingSystem.DAL.Models;
using StudentTrackingSystem.Models;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface ITeatcherRepository : IGenaricRepository<Teatcher>
    {

        // ممكن تضيف دوال خاصة بالطالب هنا لو هتحتاجها

        //IEnumerable<Student> GetAll();
        //Student GetById(int id);
        //void Add(Student student);
        //void Update(Student student);
        //void Delete(int id);
    }
}
