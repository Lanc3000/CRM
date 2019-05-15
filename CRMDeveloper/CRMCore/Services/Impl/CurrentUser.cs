using CRMCore.DB;
using CRMCore.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Services.Impl
{
    public class CurrentUser : ICurrentUser
    {
        readonly HttpContext _context;
        readonly User _user;
        readonly string _ip;
        readonly IUserRepository _userRepository;

        public CurrentUser(IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _context = httpContextAccessor.HttpContext;
            _userRepository = userRepository;
            _user = _userRepository.GetByEmail(_context.User.Identity.Name);
            _ip = _context.Connection.RemoteIpAddress.ToString();
        }

        public string Ip => _ip;
        public string FIO => _user.Fio;
        public string Email => _user.Email;
        public long UserId => (_user != null) ? _user.Id : 0;
        public bool IsAuthenticated => _user != null;
        public bool IsAdmin => _user.Role.Name == "admin";
        public bool IsManager => _user.IsManager;



        public bool IsInRole(string role)
        {
            return _context.User.IsInRole(role);
        }

    }
}
