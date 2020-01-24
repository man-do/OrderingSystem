using OrderingSystem.Data.Business.Abstract;
using OrderingSystem.Data.Data.Abstract;
using OrderingSystem.Data.Data.Entities;
using OrderingSystem.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OrderingSystem.Data.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRepository<AspNetUser> _userRepository;
        private readonly IRepository<AspNetRole> _roleRepository;
        private readonly DateTime lockoutDate = new DateTime(9999, 12, 30);

        public UserService(IRepository<AspNetUser> userRepository, IRepository<AspNetRole> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public UserViewModel GetUserViewModel (AspNetUser aspNetUser)
        {
            UserViewModel userViewModel = new UserViewModel()
            {
                Id = aspNetUser.Id,
                UserName = aspNetUser.UserName,
                Email = aspNetUser.Email,
                Role = aspNetUser.AspNetRoles.Select(role => new RoleViewModel()
                {
                    Id = role.Id,
                    Name = role.Name
                }).FirstOrDefault()
            };
            if (aspNetUser.LockoutEnabled == true && aspNetUser.LockoutEndDateUtc >= lockoutDate)
            {
                userViewModel.Disabled = true;
            }
            return userViewModel;
        }

        public IEnumerable<UserViewModel> GetAll(string _search)
        {
            List<AspNetUser> aspNetUsers = new List<AspNetUser>();

            if (_search != string.Empty && _search != null)
            {
                aspNetUsers = _userRepository.GetAll(true).Where(x => x.Email.Contains(_search)).ToList();
            }
            else
            {
                aspNetUsers = _userRepository.GetAll(true).ToList();
            }

            List<UserViewModel> result = new List<UserViewModel>();
            foreach(AspNetUser aspNetUser in aspNetUsers)
            {
                if (aspNetUser.LockoutEndDateUtc < lockoutDate || aspNetUser.LockoutEndDateUtc==null)
                {
                    result.Add(GetUserViewModel(aspNetUser));
                }
            }
            return result;
        }

        public IEnumerable<UserViewModel> GetDisabled()
        {
            List<AspNetUser> aspNetUsers = new List<AspNetUser>();
            aspNetUsers = _userRepository.GetAll(true).ToList();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (AspNetUser aspNetUser in aspNetUsers)
            {
                if (aspNetUser.LockoutEndDateUtc == lockoutDate)
                {
                    result.Add(GetUserViewModel(aspNetUser));
                }
            }
            return result;
        }

        public UserViewModel GetUserViewModel()
        {
           UserViewModel user = new UserViewModel()
            {
                Roles = _roleRepository.GetAll().ToList()
            };

            return user;
        }

        public UserViewModel GetbyId(string Id)
        {
            AspNetUser aspNetUser = _userRepository.GetById(Id);
            return GetUserViewModel(aspNetUser);
        }

        
        public void Enable(string Id)
        {
            AspNetUser aspNetUser = _userRepository.GetById(Id);
            aspNetUser.LockoutEndDateUtc = DateTime.Now;
            _userRepository.Update(aspNetUser);
            _userRepository.Save();
        }

        public void Disable (string Id)
        {
            AspNetUser aspNetUser = _userRepository.GetById(Id);
            aspNetUser.LockoutEndDateUtc = lockoutDate;
            _userRepository.Update(aspNetUser);
            _userRepository.Save();
        }

        public void Edit (UserViewModel obj)
        {
            AspNetUser aspNetUser = _userRepository.GetById(obj.Id);
            aspNetUser.Email = obj.Email;
            aspNetUser.UserName = obj.UserName;
            _userRepository.Update(aspNetUser);
            _userRepository.Save();
        }

    }
}
