using ChildScheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChildScheduler.Interfaces
{
    public interface ILoginProvider
    {
        AppUser GetUser();
        Task<bool> IsAuthorized();
        bool IsLogged();
        Task<AppUser> LoginAsync(AppUser user);
        Task<bool> RegisterAsync(AppUser user);
    }
}
