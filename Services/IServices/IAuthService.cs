using BusinessObject.Models;
using ServiceResponse;

namespace Services.IServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name);
        Task<ServiceResponse<string>> RenewToken(TokenModel model);
        Task<ServiceResponse<string>> Logout(int userId);
    }
}