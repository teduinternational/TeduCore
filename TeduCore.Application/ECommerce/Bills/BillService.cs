using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TeduCore.Application.ECommerce.Bills.Dtos;
using TeduCore.Application.ECommerce.Products.Dtos;
using TeduCore.Data.Entities;
using TeduCore.Data.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.ECommerce.Bills
{
    public class BillService : IBillService
    {
        private readonly IRepository<Bill, int> _orderRepository;
        private readonly IRepository<BillDetail, int> _orderDetailRepository;
        private readonly IRepository<Color, int> _colorRepository;
        private readonly IRepository<Size, int> _sizeRepository;
        private readonly IRepository<Product, int> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BillService(IRepository<Bill, int> orderRepository,
            IRepository<BillDetail, int> orderDetailRepository,
            IRepository<Product, int> productRepository,
            IRepository<Color, int> colorRepository,
            IRepository<Size, int> sizeRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _colorRepository = colorRepository;
            _productRepository = productRepository;
            _sizeRepository = sizeRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(BillViewModel billVm)
        {
            var order = Mapper.Map<BillViewModel, Bill>(billVm);
            var orderDetails = Mapper.Map<List<BillDetailViewModel>, List<BillDetail>>(billVm.BillDetails);
            foreach (var detail in orderDetails)
            {
                var product = _productRepository.FindById(detail.ProductId);
                detail.Price = product.PromotionPrice ?? product.Price;
            }
            order.BillDetails = orderDetails;
            _orderRepository.Add(order);
        }

        public void Update(BillViewModel billVm)
        {
            //Mapping to order domain
            var order = _orderRepository.FindById(billVm.Id, i => i.BillDetails);

            //Get order Detail
            var newDetails = Mapper.Map<List<BillDetailViewModel>, List<BillDetail>>(billVm.BillDetails);

            //new details added
            var addedDetails = newDetails.Where(x => x.Id == 0).ToList();

            //get updated details
            var updatedDetailVms = newDetails.Where(x => x.Id != 0).ToList();

            //Existed details
            var existedDetails = _orderDetailRepository.FindAll(x => x.BillId == billVm.Id);

            //Clear db

            List<BillDetail> updatedDetails = new List<BillDetail>();

            foreach (var detailVm in updatedDetailVms)
            {
                var detail = _orderDetailRepository.FindById(detailVm.Id);
                detail.Quantity = detailVm.Quantity;
                detail.ProductId = detailVm.ProductId;
                var product = _productRepository.FindById(detailVm.ProductId);
                detail.Price = product.PromotionPrice ?? product.Price;
                _orderDetailRepository.Update(detail);
                updatedDetails.Add(detail);
            }

            foreach (var detail in addedDetails)
            {
                var product = _productRepository.FindById(detail.ProductId);
                detail.Price = product.PromotionPrice ?? product.Price;
                detail.BillId = order.Id;
                _orderDetailRepository.Add(detail);
            }

            _orderDetailRepository.RemoveMultiple(existedDetails.Except(updatedDetails).ToList());

            if (order.BillStatus != BillStatus.Completed && billVm.BillStatus == BillStatus.Completed)
            {
                ConfirmBill(order.Id);
            }
            if (order.BillStatus != BillStatus.Cancelled && billVm.BillStatus == BillStatus.Cancelled)
            {
                CancelBill(order.Id);
            }
            order.CustomerName = billVm.CustomerName;
            order.CustomerAddress = billVm.CustomerAddress;
            order.CustomerFacebook = billVm.CustomerFacebook;
            order.CustomerMessage = billVm.CustomerMessage;
            order.CustomerMobile = billVm.CustomerMobile;
            order.BillStatus = billVm.BillStatus;
            order.PaymentMethod = billVm.PaymentMethod;
            order.ShippingFee = billVm.ShippingFee;

            _orderRepository.Update(order);
        }

        public void UpdateStatus(int billId, BillStatus status)
        {
            var order = _orderRepository.FindById(billId);
            order.BillStatus = status;
            _orderRepository.Update(order);
        }

        public List<SizeViewModel> GetSizes()
        {
            return _sizeRepository.FindAll().ProjectTo<SizeViewModel>().ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void ConfirmBill(int id)
        {
            var bill = _orderRepository.FindById(id, i => i.BillDetails);
            if (bill.BillStatus != BillStatus.Completed)
            {
                bill.BillStatus = BillStatus.Completed;
                foreach (var detail in bill.BillDetails)
                {
                    var product = _productRepository.FindById(detail.ProductId);
                    if (product.Quantity >= detail.Quantity)
                    {
                        product.Quantity -= detail.Quantity;
                    }
                    else
                        throw new Exception($"Sản phẩm {product.Name}-{product.Code} không đủ số lượng. Hiện tại chỉ còn {product.Quantity} trong kho.");
                }
            }
            else
            {
                throw new Exception("Đơn hàng này đã được xác nhận trước đó.");
            }
        }

        public void CancelBill(int id)
        {
            var bill = _orderRepository.FindById(id, i => i.BillDetails);
            if (bill.BillStatus != BillStatus.Cancelled)
            {
                bill.BillStatus = BillStatus.Cancelled;
                foreach (var detail in bill.BillDetails)
                {
                    var product = _productRepository.FindById(detail.ProductId);
                    product.Quantity += detail.Quantity;
                }
            }
            else
            {
                throw new Exception("Đơn này đã huỷ trước đó rồi.");
            }
        }

        public void PendingBill(int id)
        {
            var bill = _orderRepository.FindById(id, i => i.BillDetails);
            if (bill.BillStatus != BillStatus.Pending)
            {
                bill.BillStatus = BillStatus.Pending;
            }
            else
            {
                throw new Exception("Đơn hàng này đã bị hoãn trước đó.");
            }
        }

        public PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword
            , int pageIndex, int pageSize)
        {
            var query = _orderRepository.FindAll();
            if (!string.IsNullOrEmpty(startDate))
            {
                DateTime start = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated >= start);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                DateTime end = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.GetCultureInfo("vi-VN"));
                query = query.Where(x => x.DateCreated <= end);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.CustomerName.Contains(keyword) || x.CustomerMobile.Contains(keyword));
            }
            var totalRow = query.Count();
            var data = query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<BillViewModel>()
                .ToList();
            return new PagedResult<BillViewModel>()
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                Results = data,
                RowCount = totalRow
            };
        }

        public BillViewModel GetDetail(int billId)
        {
            var bill = _orderRepository.FindSingle(x => x.Id == billId);
            var billVm = Mapper.Map<Bill, BillViewModel>(bill);
            var billDetailVm = _orderDetailRepository.FindAll(x => x.BillId == billId).ProjectTo<BillDetailViewModel>().ToList();
            billVm.BillDetails = billDetailVm;
            return billVm;
        }

        public List<BillDetailViewModel> GetBillDetails(int billId)
        {
            return _orderDetailRepository
                .FindAll(x => x.BillId == billId, c => c.Bill, c => c.Product)
                .ProjectTo<BillDetailViewModel>().ToList();
        }

        public List<ColorViewModel> GetColors()
        {
            return _colorRepository.FindAll().ProjectTo<ColorViewModel>().ToList();
        }

        public BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm)
        {
            var billDetail = Mapper.Map<BillDetailViewModel, BillDetail>(billDetailVm);
            _orderDetailRepository.Add(billDetail);
            return billDetailVm;
        }

        public void DeleteDetail(int productId, int billId, int colorId, int sizeId)
        {
            var detail = _orderDetailRepository.FindSingle(x => x.ProductId == productId
           && x.BillId == billId);
            _orderDetailRepository.Remove(detail);
        }
    }
}