using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ITeatcherRepository TeatcherRepository { get; } // Add this line to define the missing property  
        Task<int> CompleteAsync();
    }

}
