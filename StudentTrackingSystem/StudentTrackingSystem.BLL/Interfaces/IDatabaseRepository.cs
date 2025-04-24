using System.Threading.Tasks;

namespace StudentTrackingSystem.BLL.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<bool> CreateBackupAsync(string? backupPath = null);
    }
}