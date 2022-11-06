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
    public class OrderRepository : IOrderRepository
    {
        private readonly cotamContext _dbContext;

        public OrderRepository(cotamContext context)
        {
            _dbContext = context;
        }
        public void ChangeTheOrderStateToCancle(int orderId)
        {
            try
            {
                OrderStates datDonThanhCong = OrderStates.DAT_DON_THANH_CONG;
                OrderStates donBiHuy = OrderStates.DON_BI_HUY;

                var order = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId);
                if (order != null)
                {
                    if (order.OrderState == Array.IndexOf(Enum.GetValues(datDonThanhCong.GetType()), datDonThanhCong))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(donBiHuy.GetType()), donBiHuy);
                        _dbContext.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void ChangeTheOrderState(int orderId)
        {
            try
            {
                OrderStates chuaDat = OrderStates.CHUA_DAT;
                OrderStates datDonThanhCong = OrderStates.DAT_DON_THANH_CONG;
                OrderStates houseworkerDaNhanDon = OrderStates.HOUSEWORKER_DA_NHAN_DON;
                OrderStates houseworkerDangDChuyen = OrderStates.HOUSEWORKER_DANG_DI_CHUYEN;
                OrderStates houseworkerDangViec = OrderStates.HOUSEWORKER_DANG_LAM_VIEC;
                OrderStates houseworkerHoanThanh = OrderStates.HOUSEWORKER_HOAN_THANH;
                OrderStates customerXacNhanDonDaHoanThanh = OrderStates.CUSTOMER_XAC_NHAN_DON_DA_HOAN_THANH;

                var order = _dbContext.Orders.FirstOrDefault(x => x.Id == orderId);
                if (order != null)
                {
                    if (order.OrderState == Array.IndexOf(Enum.GetValues(chuaDat.GetType()), chuaDat))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(datDonThanhCong.GetType()), datDonThanhCong);
                        _dbContext.SaveChanges();
                    }
                    else if (order.OrderState == Array.IndexOf(Enum.GetValues(datDonThanhCong.GetType()), datDonThanhCong))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(houseworkerDaNhanDon.GetType()), houseworkerDaNhanDon);
                        _dbContext.SaveChanges();
                    }
                    else if (order.OrderState == Array.IndexOf(Enum.GetValues(houseworkerDaNhanDon.GetType()), houseworkerDaNhanDon))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(houseworkerDangDChuyen.GetType()), houseworkerDangDChuyen);
                        _dbContext.SaveChanges();
                    }
                    else if (order.OrderState == Array.IndexOf(Enum.GetValues(houseworkerDangDChuyen.GetType()), houseworkerDangDChuyen))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(houseworkerDangViec.GetType()), houseworkerDangViec);
                        _dbContext.SaveChanges();
                    }
                    else if (order.OrderState == Array.IndexOf(Enum.GetValues(houseworkerDangViec.GetType()), houseworkerDangViec))
                    {
                        order.OrderState = Array.IndexOf(Enum.GetValues(houseworkerHoanThanh.GetType()), houseworkerHoanThanh);
                        _dbContext.SaveChanges();
                    }
                    else if (order.OrderState == Array.IndexOf(Enum.GetValues(houseworkerHoanThanh.GetType()), houseworkerHoanThanh))
                    {
                        order.EndTime = DateTime.Now;
                        order.OrderState = Array.IndexOf(Enum.GetValues(customerXacNhanDonDaHoanThanh.GetType()), customerXacNhanDonDaHoanThanh);
                        _dbContext.SaveChanges();
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public int CountOrder()
        {
            try
            {
                int count = _dbContext.Orders.Count();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetAllOrderWithPagination(int page, int pageSize)
        {
            try
            {
                var list = _dbContext.Orders.Include(x => x.House).Include(x => x.Package).Include(x => x.Promotion).Include(x => x.PaymentMethod)
                       .Skip((page - 1) * (int)pageSize)
                       .Take((int)pageSize).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                var order = _dbContext.Orders.Include(x => x.House).Include(x => x.Package).Include(x => x.Promotion).Include(x => x.PaymentMethod).FirstOrDefault(x => x.Id == id);
                if (order != null)
                { 
                    return order;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Order> GetOrdersHistoryByCusId(int houseId)
        {

            var orders = _dbContext.Orders
                .Include(x => x.House)
                .Include(x => x.Package)
                .Include(x => x.Promotion)
                .Include(x => x.PaymentMethod)
                .Where(x => x.HouseId == houseId).ToList();
            if (orders != null)
            {
                return orders;
            }
            return null;
        }
        public List<Order> GetOrdersHasStateDangDatByCusId(int houseId)
        {

            var orders = _dbContext.Orders
                .Include(x => x.House)
                .Include(x => x.Package)
                .Include(x => x.Promotion)
                .Include(x => x.PaymentMethod)
                .Where(x => x.HouseId == houseId && x.OrderState > 0 && x.OrderState < 6)
                .ToList();
            if (orders.Count > 0)
            {
                return orders;
            }
            return null;
        }
    }
}
