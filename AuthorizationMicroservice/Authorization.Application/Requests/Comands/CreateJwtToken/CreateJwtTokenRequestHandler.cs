using Authorization.Application.Abstraction.Percistance;
using Authorization.Application.Utils;
using Authorization.Domain;
using Authorization.dto;
using Common.Application.Services.Exeptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.Application.Requests.Comands.CreateJwtToken
{
    public class CreateJwtTokenRequestHandler : IRequestHandler<CreateJwtTokenComand, JwtTokenDto>
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IRepository<AppUserAppRole> _appUR;
        private readonly IRepository<AppUserRole> _roles;
        private readonly IRepository<RefreshToken> _tokens;
        private readonly IConfiguration _configuration;
        public CreateJwtTokenRequestHandler(
            IRepository<AppUser> users,
            IRepository<AppUserAppRole> appUR,
            IRepository<AppUserRole> roles,
            IRepository<RefreshToken> tokens,
            IConfiguration configuration)
        {
            _userRepository = users;
            _tokens = tokens;
            _roles = roles;
            _configuration = configuration;
            _appUR = appUR;
        }

        public async Task<JwtTokenDto> Handle(CreateJwtTokenComand request, CancellationToken cancellationToken)
        {

            var adminRole = await _roles.SingleOrDefaultAsync(i => i.Name == "Admin", cancellationToken);
            if (adminRole == null)
            {
                adminRole = await _roles.AddAsync(new AppUserRole { Name = "Admin" }, cancellationToken);
            }

            var urItem = await _appUR.FirstOrDefaultAsync(r => r.RoleId == adminRole.Id, cancellationToken);

            if(urItem == null)
            {
                var entity = new AppUser();
                entity.UpdateLogin(request.Login);
                entity.UpdatePassword(PasswordHashUtil.Hash(request.Password));
                entity.UpdatePhone("+375331234567");
                entity.UpdateEmail("admin@gmail.com");
                var addedItem = await _userRepository.AddAsync(entity, cancellationToken);
                if (addedItem is null)
                {
                    throw new BadRequestExeption("Can not add user");
                }
                var appUR = await _appUR.AddAsync(new AppUserAppRole
                {
                    Role = adminRole,
                    User = addedItem
                }, cancellationToken);
            }


            var user = await _userRepository.SingleOrDefaultAsync(u => u.Login == request.Login.Trim(), cancellationToken);
            if (user is null)
            {
                throw new NotFoundExeption($"No user with login: {request.Login}");
            }

            if (!PasswordHashUtil.Verify(request.Password, user.PasswordHash))
            {
                throw new ForbidenExeption("Incorrect authorization data");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Login),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };
            var roles = await _appUR.GetListAsync(null, null, u => u.User.Login == request.Login);
            foreach (var role in roles)
            {
                var item = await _roles.SingleOrDefaultAsync(i => i.Id == role.RoleId);
                claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var dateExpites = DateTime.UtcNow.AddMinutes(15);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDeskription = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],
                claims, expires: dateExpites, signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenDeskription)!;
            var refreshToken = await _tokens.AddAsync(new RefreshToken { UserId = user.Id, User = user }, cancellationToken);

            return new JwtTokenDto()
            {
                JwtToken = token,
                RefreshToken = refreshToken.Id,
                Expires = dateExpites
            };
        }
    }
}
