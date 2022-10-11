using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IInformationRepository
    {
        List<Information> GetAllInformationWithPagination(int page, int pageSize);
        int CountInformation();
        Information GetInformationById(int id);
        void CreateNewInformation(Information information);
        bool DisableOrEnableInformation(int inforId);
        void UpdateInformation(Information information);
    }
}
