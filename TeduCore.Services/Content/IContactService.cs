using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Services.ViewModels;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Services.Interfaces
{
    public interface IContactService
    {
        void Add(ContactDetailViewModel contactVm);

        void Update(ContactDetailViewModel contactVm);

        void Delete(string id);

        List<ContactDetailViewModel> GetAll();

        PagedResult<ContactDetailViewModel> GetAllPaging(string keyword, int page, int pageSize);

        ContactDetailViewModel GetById(string id);

        void SaveChanges();
    }
}
