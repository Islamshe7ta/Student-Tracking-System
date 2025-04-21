using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IStudentRepository StudentRepository { get; }

        public UnitOfWork(AppDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            StudentRepository = studentRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
