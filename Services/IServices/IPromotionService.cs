﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IPromotionService
    {
        Task<Response<Promotion>> GetReponsePromotionById(string id);
        Task<Response<Promotion>> GetReponseUpdatedPromotion(Promotion promotion);
    }
}