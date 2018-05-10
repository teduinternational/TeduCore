using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Application.Content.Slides.Dtos;
using TeduCore.Application.Systems.Settings.Dtos;
using TeduCore.Application.ViewModels;
using TeduCore.Data.Entities;
using TeduCore.Data.Enums;
using TeduCore.Infrastructure.Enums;
using TeduCore.Infrastructure.Interfaces;
using TeduCore.Utilities.Constants;

namespace TeduCore.Application.Systems.Commons
{
    public class CommonService : ICommonService
    {
        private IRepository<Footer, string> _footerRepository;
        private IRepository<Setting, Guid> _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<Slide, Guid> _slideRepository;

        public CommonService(IRepository<Footer, string> footerRepository,
            IRepository<Setting, Guid> systemConfigRepository,
            IUnitOfWork unitOfWork,
            IRepository<Slide, Guid> slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
        }

        public FooterViewModel GetFooter()
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.Single(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<SlideViewModel> GetSlides(SlideGroup groupAlias)
        {
            return _slideRepository.GetAll().Where(x => x.Status == Status.Actived && x.GroupAlias == groupAlias).OrderBy(x => x.DisplayOrder)
                .ProjectTo<SlideViewModel>().ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return Mapper.Map<Setting, SystemConfigViewModel>(_systemConfigRepository.Single(x => x.UniqueCode == code));
        }
    }
}