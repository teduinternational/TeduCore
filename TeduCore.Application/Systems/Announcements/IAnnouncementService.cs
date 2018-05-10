using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;

namespace TeduCore.Application.Systems.Announcements
{
    public interface IAnnouncementService
    {
        void Create(Announcement announcement);

        List<Announcement> GetListByUserId(string userId, int pageIndex, int pageSize, out int totalRow);

        List<Announcement> GetListByUserId(string userId, int top);

        void Delete(string notificationId);

        void MarkAsRead(string userId, string notificationId);

        Announcement GetDetail(string id);

        List<Announcement> GetListAll(int pageIndex, int pageSize, out int totalRow);

        List<Announcement> ListAllUnread(string userId, int pageIndex, int pageSize, out int totalRow);

        void Save();

    }

}
