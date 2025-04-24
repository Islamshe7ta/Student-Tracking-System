using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentTrackingSystem.BLL.Interfaces;
using StudentTrackingSystem.DAL.Data.Contexts;

namespace StudentTrackingSystem.BLL.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly AppDbContext _context;
        private readonly string _defaultBackupFolder;

        public DatabaseRepository(AppDbContext context)
        {
            _context = context;
            _defaultBackupFolder = Path.Combine(Directory.GetCurrentDirectory(), "Backups");

            if (!Directory.Exists(_defaultBackupFolder))
            {
                Directory.CreateDirectory(_defaultBackupFolder);
            }
        }

        public async Task<bool> CreateBackupAsync(string? backupPath = null)
        {
            try
            {
                var actualPath = backupPath ?? Path.Combine(_defaultBackupFolder, $"backup_{DateTime.Now:yyyyMMddHHmmss}.bak");

                // Secure SQL execution with parameters
                var databaseName = _context.Database.GetDbConnection().Database;
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"BACKUP DATABASE [{databaseName}] TO DISK = {actualPath}");

                return true;
            }
            catch (Exception ex)
            {
                // سجل الخطأ هنا
                Console.WriteLine($"Error during backup: {ex.Message}");
                // يمكنك أيضاً استخدام إطار عمل لتسجيل الأخطاء مثل Serilog أو NLog

                return false;
            }
        }
    }
}