using backend.Src.Data;
using System.Text;
using backend.Src.Repositories;
using backend.Src.Repositories.Interfaces;
using backend.Src.Services;
using backend.Src.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;


namespace backend.Src.Extensions
{
    // Extensión para agregar los servicios de la aplicación.
    public static class AppServiceExtensions
    {
        /// <summary>
        /// Método para agregar los servicios de la aplicación.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param>
        /// <param name="config">
        /// - config: Configuración de la aplicación.
        /// </param>
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            AddSwaggerGen(services);
            AddDbContext(services);
            AddRepositories(services);
            AddServices(services);
            AddAuthentication(services, config);
            AddAutoMapper(services);
        }

        /// <summary>
        /// Método para agregar los servicios de swagger.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param>
        private static void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Método para agregar el contexto de la base de datos.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param> 
        private static void AddDbContext(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlite("Data Source=MobileHub.db"));
        }


        /// <summary>
        /// Método para agregar los repositorios de la aplicación.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param>
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IReposRepository, ReposRepository>();
        }

        /// <summary>
        /// Método para agregar los servicios de la aplicación.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param>
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IMappingService, MappingService>();
            services.AddScoped<IReposService, ReposService>();
        }

        /// <summary>   
        /// Método para agregar la autenticación.
        /// </summary>
        /// <param name="services">
        /// - services: Servicios de la aplicación.
        /// </param>
        /// <param name="config">
        /// - config: Configuración de la aplicación.
        /// </param>
        private static IServiceCollection AddAuthentication(IServiceCollection services, IConfiguration config)
        {
            Env.Load();
            var jwtSecret = Env.GetString("SECRET_KEY") ?? throw new Exception("SECRET_KEY is null");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
        }


    }
}