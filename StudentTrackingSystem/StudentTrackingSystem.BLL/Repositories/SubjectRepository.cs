using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTrackingSystem.DAL.Models;


namespace StudentTrackingSystem.BLL.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;

        public SubjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }
        public async Task<Subject> GetAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Teatchers)  // تأكد من تضمين المعلمين
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
        }

        public void Update(Subject subject)
        {
            _context.Subjects.Update(subject);
           _context.SaveChangesAsync();
        }
        public void Delete(Subject subject)
        {
            _context.Subjects.Remove(subject);
            _context.SaveChangesAsync();
        }



    }
}

