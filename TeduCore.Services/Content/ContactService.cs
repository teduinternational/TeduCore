using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Interfaces;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Implementation
{
    public class ContactService : IContactService
    {
        private IRepository<ContactDetail, string> _pageRepository;
        private IUnitOfWork _unitOfWork;

        public ContactService(IRepository<ContactDetail, string> pageRepository,
            IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(ContactDetailViewModel pageVm)
        {
            var page = Mapper.Map<ContactDetailViewModel, ContactDetail>(pageVm);
            _pageRepository.Add(page);
        }

        public void Delete(string id)
        {
            _pageRepository.Remove(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<ContactDetailViewModel> GetAll()
        {
            return _pageRepository.FindAll().ProjectTo<ContactDetailViewModel>().ToList();
        }

        public PagedResult<ContactDetailViewModel> GetAllPaging(string keyword, int page, int pageSize)
        {
            var query = _pageRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword));

            int totalRow = query.Count();
            var data = query.OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            var paginationSet = new PagedResult<ContactDetailViewModel>()
            {
                Results = data.ProjectTo<ContactDetailViewModel>().ToList(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public ContactDetailViewModel GetById(string id)
        {
            return Mapper.Map<ContactDetail, ContactDetailViewModel>(_pageRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ContactDetailViewModel pageVm)
        {
            var page = Mapper.Map<ContactDetailViewModel, ContactDetail>(pageVm);
            _pageRepository.Update(page);
        }
    }
}