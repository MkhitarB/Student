using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student.DAL.Repository;
using Student.DTO.Authentication;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;
using Student.Infrastructure;
using Student.Infrastructure.Exceptions;
using Student.Infrastructure.Helpers.Utilities;
using Student.Infrastructure.NewFolder;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, GetTokenResModel>
    {
        private readonly IEntityRepository _repository;
        private readonly TokenProviderOptions _options;
        public GetTokenQueryHandler(IEntityRepository repository,
            IOptions<TokenProviderOptions> options,
            IOptions<AppSettings> settings,
            IHostingEnvironment environment)
        {
            _repository = repository;
            _options = options.Value;
            _options.SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            _options.Audience = "StudentReactJsWebApp";
            _options.Issuer = "Student" + environment.EnvironmentName;
        }

        public async Task<GetTokenResModel> Handle(GetTokenQuery query, CancellationToken token)
        {
            try
            {

                var user = await _repository.FilterAsNoTracking<User>(u => u.Email == query.model.Email)
                    .Select(u => new
                    {
                        u.Id,
                        u.Password,
                        u.Type
                    }).FirstOrDefaultAsync();

                if (user == null)
                    throw new SmartException("Email is incorrect");

                var success = Util.VerifyHashedPassword(user.Password, query.model.Password);

                if (!success)
                    throw new SmartException("Password is incorrect");

                var now = DateTime.UtcNow;

                var claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub, query.model.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.Second.ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.NameIdentifier, query.model.Email),
                new Claim(ClaimTypes.Actor, user.Id.ToString()),
                new Claim("personId", user.Id.ToString()),
                new Claim("type", user.Type.ToString()),
                new Claim("ExpireDate", now.Add(_options.Expiration).ToString(CultureInfo.InvariantCulture))
            };
                var jwt = new JwtSecurityToken(

                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(_options.Expiration),
                    signingCredentials: _options.SigningCredentials);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var result = new GetTokenResModel
                {
                    Token = encodedJwt,
                    ExpireDate = now.Add(_options.Expiration),
                    UserType = user.Type
                };

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
