using System;
using System.Collections.Generic;
using System.Text;

namespace CRMCore.Services
{
    public interface ICurrentUser
    {
        long UserId { get; }

        string FIO { get; }
        string Email { get; }
        bool IsAuthenticated { get; }

        bool IsAdmin { get; }
        bool IsManager { get; }

        bool IsInRole(string role);
    }
}
