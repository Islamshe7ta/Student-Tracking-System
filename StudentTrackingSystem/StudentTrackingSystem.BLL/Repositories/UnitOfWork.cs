using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using StudentTrackingSystem.DAL.Models;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IStudentRepository StudentRepository { get; private set; }
        public ITeatcherRepository TeatcherRepository { get; private set; }
        public ISubjectRepository SubjectRepository { get; private set; }
        public IParentRepository ParentRepository { get; private set; }

        public UnitOfWork(AppDbContext context, IStudentRepository studentRepository, ITeatcherRepository teatcherRepository, ISubjectRepository subject, IParentRepository parentRepository)
        {
            _context = context;

            StudentRepository = studentRepository;
            TeatcherRepository = teatcherRepository;
            SubjectRepository = subject;
            ParentRepository = parentRepository;  // Only one reference to ParentRepository
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Teatcher>> GetAllWithSubjectAsync()
        {
            throw new NotImplementedException();
        }
    }


}
