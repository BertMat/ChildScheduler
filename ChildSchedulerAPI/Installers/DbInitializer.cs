using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ChildSchedulerAPI.Installers
{
    public class DbInitializer
    {
        public async static Task Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SchedulerContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
