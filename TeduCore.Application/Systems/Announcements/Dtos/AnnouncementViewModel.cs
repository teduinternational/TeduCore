using System;
using System.Collections.Generic;
using TeduCore.Application.Systems.Users.Dtos;

namespace TeduCore.Application.Systems.Announcements.Dtos
{
    public class AnnouncementViewModel
    {
        public AnnouncementViewModel()
        {
            AnnouncementUsers = new List<AnnouncementUserViewModel>();
        }

        public Guid id { set; get; }

        public string Title { set; get; }

        public string Content { set; get; }

        public DateTime CreatedDate { get; set; }

        public string UserId { set; get; }

        public AppUserViewModel AppUser { get; set; }

        public bool Status { get; set; }

        public virtual ICollection<AnnouncementUserViewModel> AnnouncementUsers { get; set; }
    }
}