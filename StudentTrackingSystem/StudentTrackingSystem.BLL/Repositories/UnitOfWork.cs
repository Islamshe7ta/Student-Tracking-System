using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly ITeatcherRepository _teatcherRepository;
        private readonly ISubjectRepository _subject;
        private readonly IStudentRepository _studentRepository;
        public IStudentRepository StudentRepository { get; private set; }

        public ITeatcherRepository TeatcherRepository { get; private set; }

        public ISubjectRepository SubjectRepository { get; private set; }

        public UnitOfWork(AppDbContext context, IStudentRepository studentRepository, ITeatcherRepository teatcherRepository, ISubjectRepository subject)
        {
            _context = context;

            StudentRepository = studentRepository;
            TeatcherRepository = teatcherRepository;
            SubjectRepository = subject;
        }



        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
