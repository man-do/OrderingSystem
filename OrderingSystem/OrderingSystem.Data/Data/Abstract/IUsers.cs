using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Data.Abstract
{
    public interface IUsers
    {
        IEnumerable<UserViewModel> GetAll();
        UserViewModel GetbyId(string Id);
    }
}
