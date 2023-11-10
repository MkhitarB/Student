using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Student.Application.Middlewares;
using System.Text;

namespace Student.Extensions
{
    internal static class JwtExtensions
    {
        internal static void ConfigureJwtToken(this IServiceCollection services, IHostEnvironment environment)
        {
            var secretKey = "mysupersecret_secretkey!PT";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = "Student" + environment.EnvironmentName,
                ValidateAudience = true,
                ValidAudience = "StudentReactJsWebApp",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.TokenValidationParameters = tokenValidationParameters;
                c.RequireHttpsMetadata = false;
                c.Events = new JwtBearerEvents
                {
                   
                };
            });

            services.AddAuthorization(o =>
            {
                o.AddPolicy("CheckUnauthorized", p =>
                {
                    p.Requirements.Add(new AuthorizeRequirement());
                });
            });

        }
    }
}
