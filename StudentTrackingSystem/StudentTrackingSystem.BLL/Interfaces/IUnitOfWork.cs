using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }   // لاحظ اسم الـ Property
        Task<int> CompleteAsync();
    }

}
