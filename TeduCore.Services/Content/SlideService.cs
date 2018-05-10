﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Interfaces;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Implementation
{
    public class SlideService : ISlideService
    {
        private readonly IRepository<Slide,int> _slideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SlideService(IRepository<Slide,int> slideRepository,
            IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public void Add(SlideViewModel slideVm)
        {
            _slideRepository.Add(Mapper.Map<SlideViewModel, Slide>(slideVm));
        }

        public void Update(SlideViewModel slideVm)
        {
            _slideRepository.Update(Mapper.Map<SlideViewModel, Slide>(slideVm));
        }

        public void Delete(int id)
        {
            _slideRepository.Remove(id);
        }

        public List<SlideViewModel> GetAll()
        {
            return _slideRepository.FindAll()
                .ProjectTo<SlideViewModel>().ToList();
        }

        public PagedResult<SlideViewModel> GetAllPaging(string keyword, int page, int pageSize, string sortBy)
        {
            var query = _slideRepository.FindAll();
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword) || x.GroupAlias.Contains(keyword));

            int totalRow = query.Count();

            query = query.OrderByDescending(x=>x.Id).Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = query.ProjectTo<SlideViewModel>().ToList();
            var paginationSet = new PagedResult<SlideViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public SlideViewModel GetById(int id)
        {
            return Mapper.Map<Slide, SlideViewModel>(_slideRepository.FindById(id));
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}