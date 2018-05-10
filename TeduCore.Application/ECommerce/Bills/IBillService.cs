using System;
using System.Collections.Generic;
using TeduCore.Application.ECommerce.Bills.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Entities;
using TeduCore.Data.Enums;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.ECommerce.Bills
{
    public interface IBillService: IWebServiceBase<Bill, Guid, BillViewModel>
    {
        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        BillViewModel GetDetail(Guid billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(Guid productId, Guid billId);

        void UpdateStatus(Guid orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(Guid billId);

        void ConfirmBill(Guid id);

        void CancelBill(Guid id);

        void PendingBill(Guid id);
    }
}