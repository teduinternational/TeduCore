using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TeduCore.Data.Entities;
using TeduCore.Infrastructure.Interfaces;

namespace TeduCore.Application.Systems.Announcements
{
    public class AnnouncementService : IAnnouncementService
    {
        private IRepository<Announcement, string> _announcementRepository;
        private IRepository<AnnouncementUser, int> _announcementUserRepository;

        private IUnitOfWork _unitOfWork;

        public AnnouncementService(IRepository<Announcement, string> announcementRepository,
            IRepository<AnnouncementUser, int> announcementUserRepository,
            IUnitOfWork unitOfWork)
        {
            _announcementRepository = announcementRepository;
            _announcementUserRepository = announcementUserRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(Announcement announcement)
        {
            _announcementRepository.Add(announcement);
        }

        public void Delete(string notificationId)
        {
            _announcementRepository.Remove(notificationId);
        }

        public List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.FindAll(x => x.AppUser);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(string userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = _announcementRepository.FindAll(x => x.UserId == userId);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        public List<Announcement> GetListByUserId(string userId, int top)
        {
            return _announcementRepository.FindAll(x => x.UserId == userId)
                .OrderByDescending(x => x.DateCreated)
                .Take(top).ToList();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public Announcement GetDetail(string id)
        {
            return _announcementRepository.FindSingle(x => x.Id == id, x => x.AppUser);
        }

        public List<Announcement> ListAllUnread(string userId, int pageIndex, int pageSize, out int totalRow)
        {
            var query = (from x in _announcementRepository.FindAll()
                         join y in _announcementUserRepository.FindAll()
                         on x.Id equals y.AnnouncementId
                         into xy
                         from y in xy.DefaultIfEmpty()
                         where (y.HasRead == null || y.HasRead == false)
                         && (y.UserId == null || y.UserId == userId)
                         select x).Include(x => x.AppUser);
            totalRow = query.Count();
            return query.OrderByDescending(x => x.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
        }

        public void MarkAsRead(string userId, string notificationId)
        {
            var announ = _announcementUserRepository.FindSingle(x => x.AnnouncementId == notificationId && x.UserId == userId);
            if (announ == null)
            {
                _announcementUserRepository.Add(new AnnouncementUser()
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