namespace Agilis.Core.Domain.Abstractions.Services
{
    public interface IAdminDatabaseService
    {
        void DropDatabase(string name);
    }
}