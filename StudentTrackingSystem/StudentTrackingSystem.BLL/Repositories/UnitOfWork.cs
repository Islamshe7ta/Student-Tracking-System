using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ITeatcherRepository _teatcherRepository;
        private readonly IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository { get; private set; }

        public ITeatcherRepository TeatcherRepository { get; private set; }

        public UnitOfWork(AppDbContext context, IStudentRepository studentRepository , ITeatcherRepository teatcherRepository)
        {
            _context = context;
            _teatcherRepository = teatcherRepository;
            StudentRepository = studentRepository;
            TeatcherRepository = teatcherRepository;


        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
