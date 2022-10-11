using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly cotamContext _dbContext;

        public InformationRepository(cotamContext cotamContext)
        {
            _dbContext = cotamContext;
        }
        public int CountInformation()
        {
            try
            {
                int count = _dbContext.Information.Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void CreateNewInformation(Information information)
        {
            try
            {
                _dbContext.Information.Add(information);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DisableOrEnableInformation(int inforId)
        {
            try
            {
                var infor = _dbContext.Information.FirstOrDefault(x => x.Id == inforId);
                if (infor != null)
                {
                    if (infor.Active == true)
                    {
                        infor.Active = false;
                        _dbContext.SaveChanges();
                        return true;
                    }
                    else if (infor.Active == false)
                    {
                        infor.Active = true;
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Information> GetAllInformationWithPagination(int page, int pageSize)
        {
            try
            {
                var list = _dbContext.Information
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Information GetInformationById(int id)
        {
            try
            {
                var infor = _dbContext.Information.FirstOrDefault(x => x.Id == id);
                if (infor != null)
                {
                    return infor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateInformation(Information information)
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
