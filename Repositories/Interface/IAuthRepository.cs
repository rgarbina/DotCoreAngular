using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular_ASPNETCore.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Login(string username, string password);
    }
}
