using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IChildService, ChildService>();
            services.AddScoped<IChildHistoryService, ChildHistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISocialMediaService, SocialMediaService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ICostService, CostService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
