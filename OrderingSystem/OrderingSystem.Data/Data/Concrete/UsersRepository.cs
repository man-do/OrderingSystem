using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Data.Concrete
{
    public class UsersRepository : IUsers
    {
            private OSEntities context = null;
            private DbSet<UserViewModel> table = null;

            public UsersRepository()
            {
                context = new OSEntities();
                table = context.Set<UserViewModel>();
            }

            public IEnumerable<UserViewModel> GetAll()
            {

            // var User = context.AspNetUsers.Include(r => r.AspNetRoles).FirstOrDefault();
            var Role = context.AspNetRoles.Include(x => x.AspNetUsers);
            var Data = context.AspNetUsers.Select(x => new UserViewModel()
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                PasswordHash = x.PasswordHash,
                LockoutEnabled = x.LockoutEnabled,
                LockoutEndDateUtc = x.LockoutEndDateUtc,
                Role = new RoleViewModel()
                {
                    Id = x.AspNetRoles.FirstOrDefault().Id,
                    Name = x.AspNetRoles.FirstOrDefault().Name
                }
            }).ToList();

            return Data;
        }

             public UserViewModel GetbyId(string id)
            {
                UserViewModel foundItem = table.Find(id);
                return foundItem;
            }


    }
}
