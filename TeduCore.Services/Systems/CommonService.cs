using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;

using TeduCore.Infrastructure.Interfaces;
using TeduCore.Services.Interfaces;
using TeduCore.Services.ViewModels;
using TeduCore.Services.ViewModels.System;
using TeduCore.Utilities.Constants;

namespace TeduCore.Services.Implementation
{
    public class CommonService : ICommonService
    {
        private IRepository<Footer, string> _footerRepository;
        private IRepository<SystemConfig, string> _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        private IRepository<Slide, int> _slideRepository;

        public CommonService(IRepository<Footer, string> footerRepository,
            IRepository<SystemConfig, string> systemConfigRepository,
            IUnitOfWork unitOfWork,
            IRepository<Slide, int> slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
        }

        public FooterViewModel GetFooter()
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<SlideViewModel> GetSlides(string groupAlias)
        {
            return _slideRepository.FindAll(x => x.Status && x.GroupAlias == groupAlias).OrderBy(x => x.DisplayOrder)
                .ProjectTo<SlideViewModel>().ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return Mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }
    }
}