using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IStudentRepository StudentRepository { get; private set; }
        public ITeatcherRepository TeatcherRepository { get; private set; }
        public IAuditLogRepository AuditLogRepository { get; private set; }
        public IDatabaseRepository DatabaseRepository { get; private set; }
        public ISystemSettingRepository SystemSettingRepository { get; private set; }
        public IUserRepository UserRepository { get; private set; }

        public UnitOfWork(
            AppDbContext context,
            IStudentRepository studentRepository,
            ITeatcherRepository teatcherRepository,
            IAuditLogRepository auditLogRepository,
            IDatabaseRepository databaseRepository,
            ISystemSettingRepository systemSettingRepository,
            IUserRepository userRepository)
        {
            _context = context;
            StudentRepository = studentRepository;
            TeatcherRepository = teatcherRepository;
            AuditLogRepository = auditLogRepository;
            DatabaseRepository = databaseRepository;
            SystemSettingRepository = systemSettingRepository;
            UserRepository = userRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
