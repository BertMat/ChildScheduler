using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Repositories
{
    public interface ICurrentUserService
    {
        string GetCurrentUserId(HttpContext context);
    }
}
