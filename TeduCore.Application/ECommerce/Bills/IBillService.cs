using System.Collections.Generic;
using TeduCore.Application.ECommerce.Bills.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Enums;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.ECommerce.Bills
{
    public interface IBillService
    {
        void Create(BillViewModel billVm);

        void Update(BillViewModel billVm);

        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        BillViewModel GetDetail(int billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(int productId, int billId, int colorId, int sizeId);

        void UpdateStatus(int orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(int billId);

        List<ColorViewModel> GetColors();

        List<SizeViewModel> GetSizes();

        void Save();

        void ConfirmBill(int id);

        void CancelBill(int id);

        void PendingBill(int id);
    }
}