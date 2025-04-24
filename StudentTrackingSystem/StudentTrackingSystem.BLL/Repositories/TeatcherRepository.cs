using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;
using StudentTrackingSystem.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class TeatcherRepository : ITeatcherRepository
    {
        private readonly AppDbContext _context;

        public TeatcherRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Teatcher>> GetAllAsync()
        {
            return await _context.Teatchers.ToListAsync();
        }

        public async Task<Teatcher?> GetAsync(int id)
        {
            return await _context.Teatchers.FindAsync(id);
        }

        public async Task AddAsync(Teatcher entity)
        {
            await _context.Teatchers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(Teatcher entity)
        {
            _context.Teatchers.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Teatcher entity)
        {
            _context.Teatchers.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Teatchers.CountAsync();
        }

        public async Task<List<Teatcher>> GetRecentAsync(int count)
        {
            return await _context.Teatchers
                                .OrderByDescending(t => t.Id)
                                .Take(count)
                                .ToListAsync();
        }
    }
}