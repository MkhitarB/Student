using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Student.Application.Middlewares;
using Student.BLL.Mediator;
using Student.BLL.Services.UserService.Services;
using Student.DAL;
using Student.DAL.Repository;
using Student.Infrastructure.NewFolder;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Application.ApplicationProgram
{
    public static class ProgramHelper
    {
        public static void RegisterComponents(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, ApiAuthorize>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IEntityDbContext, EntityDbContext>()
               .AddScoped(x => new Lazy<IEntityDbContext>(x.GetRequiredService<IEntityDbContext>));

            services.AddScoped<IMediatorHandler, MediatorHandler>()
               .AddScoped(x => new Lazy<IMediatorHandler>(x.GetRequiredService<IMediatorHandler>));




            #region Repositories
            services.AddScoped<IEntityRepository, EntityRepository>()
               .AddScoped(x => new Lazy<IEntityRepository>(x.GetRequiredService<IEntityRepository>));


            #endregion

            #region Services
            services.AddScoped<IUserService, UserService>()
                           .AddScoped(x => new Lazy<IUserService>(x.GetRequiredService<IUserService>));

            

            #endregion

        }

        public static void IsServerConnected()
        {
            using (var connection = new SqlConnection(ConstValues.ConnectionString))
            {
                try
                {
                    connection.Open();
                    ConstValues.IsServerConnected = true;
                }
                catch (Exception)
                {
                    ConstValues.IsServerConnected = false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
