using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Business.Abstract
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAll(string _search);
        IEnumerable<UserViewModel> GetDisabled();
        UserViewModel GetUserViewModel();
        UserViewModel GetbyId(string Id);
        void Disable(string Id);
        void Enable(string Id);
        void Edit(UserViewModel obj);

    }
}
