using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Repository.IRepositories;
using Repository.Repositories;
using Repository.UnitOfWork;
using Service.IServices;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public static class RegisterDependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<ITalentService, TalentService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IStarService, StarService>();
            services.AddScoped<IPreferenceService, PreferenceService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IExcelServiceUtil, ExcelServiceUtil>();
            services.AddScoped<IRetentionService, RetentionService>();

            
            #region Add All Dependency Injections
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion
        }
    }
}
