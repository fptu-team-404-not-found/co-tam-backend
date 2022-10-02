using BusinessObject.Models;
using ServiceResponse;

namespace Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name);
        Task<ServiceResponse<AdminManager>> GetAdminManager(int id);
        
    }
}