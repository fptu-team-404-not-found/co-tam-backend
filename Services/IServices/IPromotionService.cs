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
        Task<Response<List<Promotion>>> GetReponsePromotions(int pageIndex, int pageSize);
        Task<Response<List<Promotion>>> GetReponsePromotionsVerMobile(int pageIndex, int pageSize);
        Task<Response<Promotion>> GetResponseCreateAPromotion(Promotion promotion);
        Task<Response<Promotion>> GetReponseUpdatedPromotion(string id, Promotion promotion);
        Task<Response<Promotion>> GetReponseChangeStatusPromotion(string id);
        Task<Response<int>> GetResponsePromotionNumber();
        Task<Response<int>> CountPromotionsVerMobile();
        Task<Response<List<Promotion>>> SearchPromotion(string searchString, int page, int pageSize);
        Task<Response<int>> CountPromotionWhenSearch(string searchString);
    }
}
