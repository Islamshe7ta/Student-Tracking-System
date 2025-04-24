using System;
using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ITeatcherRepository TeatcherRepository { get; }
        IAuditLogRepository AuditLogRepository { get; }
        IDatabaseRepository DatabaseRepository { get; }
        ISystemSettingRepository SystemSettingRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> CompleteAsync();
    }
}
