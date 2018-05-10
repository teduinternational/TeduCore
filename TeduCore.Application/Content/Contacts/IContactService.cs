using System.Collections.Generic;
using TeduCore.Application.Content.Contacts.Dtos;
using TeduCore.Utilities.Dtos;

namespace TeduCore.Application.Content.Contacts
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