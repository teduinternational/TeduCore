using System;
using System.Collections.Generic;
using System.Text;
using TeduCore.Data.Entities;

namespace TeduCore.Services.Interfaces
{
    public interface IErrorService
    {
        void Create(Error error);

        void Save();
    }
}
