using DC.PicpaySim.Application;
using DC.PicpaySim.Application.Handlers.Auth;
using DC.PicpaySim.Application.Handlers.User;
using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Interfaces;
using DC.PicpaySim.Domain.Validators;
using DC.PicpaySim.Infrastructure.Middleware;
using DC.PicpaySim.Infrastructure.ORM;
using DC.PicpaySim.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DC.PicpaySim.API.DI
{
    public static class Bootstrap
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddDbContext<DatabaseContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default"))
           );

            var stringToken = configuration["JWT:token"];

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(o =>
                {
                    var key = Encoding.UTF8.GetBytes(stringToken);
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                }
            );

            AddValidator(services);
            AddRepositories(services);
            AddServices(services);
        }

        private static void AddValidator(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddTransient<IValidator<GetUserQuery>, GetUserQueryValidator>();

            services.AddTransient<IValidator<AuthUserCommand>, AuthUserValidator>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateUserCommand, BaseResponse<UserDTO>>, CreateUserHandler>();
            services.AddTransient<IRequestHandler<GetUserQuery, BaseResponse<UserDTO>>, GetUserHandler>();
            services.AddTransient<IRequestHandler<GetUserByDocumentQuery, BaseResponse<UserDTO>>, GetUserByDocumentHandler>();

            services.AddTransient<IRequestHandler<AuthUserCommand, BaseResponse<UserAuthDTO>>, AuthUserHandler>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

    }
}
