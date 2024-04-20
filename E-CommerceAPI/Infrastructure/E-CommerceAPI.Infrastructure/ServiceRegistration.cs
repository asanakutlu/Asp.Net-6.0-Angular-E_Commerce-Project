using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceAPI.Infrastructure.Services.Storage;
using E_CommerceAPI.Infrastructure.Enums;
using E_CommerceAPI.Infrastructure.Services.Storage.Local;
using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Infrastructure.Services.Token;
using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Abstractions.Services.Configurations;
using E_CommerceAPI.Infrastructure.Services.Configurations;

namespace E_CommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageServices, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();
            serviceCollection.AddScoped<IQRCodeService, QRCodeService>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage,Services.Storage.Azure.AzureStorage>();
                    break;
                case StorageType.AWS:
                    
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;

            }
        }
    }
}
