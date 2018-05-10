using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;

namespace TeduCore.Application.Systems.Announcements
{
    public class AnnouncementService : IAnnouncementService
    {
        private IRepository<Announcement, Guid> _announcementRepository;
        private IRepository<AnnouncementUser, Guid> _announcementUserRepository;

        private IUnitOfWork _unitOfWork;

        public AnnouncementService(IRepository<Announcement, Guid> announcementRepository,
            IRepository<AnnouncementUser, Guid> announcementUserRepository,
            IUnitOfWork unitOfWork)
        {
            _announcementRepository = announcementRepository;
            _announcementUserRepository = announcementUserRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(Announcement announcement)
        {
            _announcementRepository.Insert(announcement);
        }

        public void Delete(Guid notificationId)
        {
            _announcementRepository.Delete(notificationId);
        }

        public List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.GetAll();
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(Guid userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.GetAll().Where(x => x.OwnerId == userId);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(Guid userId, int top)
        {
            return _announcementRepository.GetAll().Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.DateCreated)
                .Take(top).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Announcement GetDetail(Guid id)
        {
            return _announcementRepository.Single(x => x.Id == id);
        }

        public List<Announcement> ListAllUnread(Guid userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _announcementRepository.GetAll()
                         join y in _announcementUserRepository.GetAll()
                         on x.Id equals y.AnnouncementId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where (y.HasRead == null || y.HasRead == false)
                         && (y.UserId == null || y.UserId == userId)
                         select x);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public void MarkAsRead(Guid userId, Guid notificationId)
        {
            var announ = _announcementUserRepository.Single(x => x.AnnouncementId == notificationId && x.UserId == userId);
            if (announ == null)
            {
                _announcementUserRepository.Insert(new AnnouncementUser()
                {
                    AnnouncementId = notificationId,
                    UserId = userId,
                    HasRead = true
                });
            }
            else
            {
                announ.HasRead = true;
            }
        }
    }
}