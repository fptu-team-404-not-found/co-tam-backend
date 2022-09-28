using CoTamApp;
using CoTamApp.Models;
using Microsoft.EntityFrameworkCore;
using ServiceResponse;

namespace Repositories
{
    public class AuthRepository
    {
        private static AuthRepository instance = null;
        private static readonly object instanceLock = new object();
        

        private AuthRepository()
        {
            
        }
        
        public static AuthRepository Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AuthRepository();
                    }
                    return instance;
                }
            }
        }
        public async Task<ServiceResponse<string>> LoginWithAdminManager(string email, string name)
        {
            var _dbContext = new cotamContext();
            var accountAdminManager = await _dbContext.AdminManagers.FirstOrDefaultAsync(x => x.Email == email);
            if (accountAdminManager != null && accountAdminManager.RoleId == 1)
            {
                return new ServiceResponse<string>
                {
                    Data = accountAdminManager.Email,
                    Success = true,
                    Message = "Login successfully with admin account"
                };
            }
            else if (accountAdminManager != null && accountAdminManager.RoleId == 2)
            {
                return new ServiceResponse<string>
                {
                    Data = accountAdminManager.Email,
                    Success = true,
                    Message = "Login successfully with manager account"
                };
            }
            return new ServiceResponse<string>
            {
                Success = false,
                Message = "Login failed"
            };
        }
        public async Task<ServiceResponse<AdminManager>> GetAdminManager(int id)
        {
            var _dbContext = new cotamContext();
            var ad = await _dbContext.AdminManagers.FirstOrDefaultAsync(x => x.Id == id);
            return new ServiceResponse<AdminManager>
            {
                Data = ad,
                Message = "Successfully",
                Success = true
                
            };
        }
    }
}