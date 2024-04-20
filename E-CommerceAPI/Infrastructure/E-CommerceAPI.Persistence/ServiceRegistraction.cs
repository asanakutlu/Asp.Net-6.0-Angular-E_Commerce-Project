using E_CommerceAPI.Application.Abstractions;
using E_CommerceAPI.Persistence.Concretes;
using Microsoft.EntityFrameworkCore;
using E_CommerceAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Persistence.Repositories;
using E_CommerceAPI.Domain.Entities.Identity;
using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Persistence.Services;
using E_CommerceAPI.Application.Abstractions.Services.Authentications;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceAPI.Persistence
{
    public static class ServiceRegistraction
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            
            services.AddDbContext<E_CommerceAPIDbContext>(options => options.UseNpgsql(Configration.connectionstring));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase=false;
                options.Password.RequireUppercase=false;
               
            }).AddEntityFrameworkStores<E_CommerceAPIDbContext>().AddDefaultTokenProviders();
            services.AddScoped<ICustomerReadRepository,CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository,OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository,OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriterRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();
            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriterRepository, BasketItemWriterRepository>();
            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriterRepository, BasketWriterRepository>();
            services.AddScoped<ICompetedOrderWriteRepository, CompletedOrderWriteRepository>();
            services.AddScoped<ICompetedOrderReadRepository, CompletedOrderReadRepository>();


            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<IExternalAuthentication,AuthService>();
            services.AddScoped<IInternalAuthentication,AuthService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRoleService, RoleService>();  
            services.AddScoped<IProdeucttService, ProducttService>();  

        }
    }
}
