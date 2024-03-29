﻿using BusinessObject.Models;
using ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepositories
{
    public interface IHouseRepository
    {
        House GetHouseById(int id);
        List<House> GetListByCustomerId(int customerId, int page, int pageSize);
        bool CreateHouse(House house);
        void UpdateHouse(House house);
        void DeleteHouse(int houseId);
        int CountHouse(int customerId);
        List<House> GetHouseByCusId(int cusId);
    }
}
