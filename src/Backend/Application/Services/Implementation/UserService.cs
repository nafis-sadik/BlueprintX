using Data.Entities;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RedBook.Core.AutoMapper;
using RedBook.Core.Constants;
using RedBook.Core.Domain;
using RedBook.Core.Security;
using RedBook.Core.UnitOfWork;
using Services.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services.Implementation
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(
            ILogger<UserService> logger,
            IObjectMapper mapper,
            IUnitOfWorkManager unitOfWork,
            IClaimsPrincipalAccessor claimsPrincipalAccessor,
            IHttpContextAccessor httpContextAccessor
        ) : base(logger, mapper, claimsPrincipalAccessor, unitOfWork, httpContextAccessor) { }

        public Task DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Login(LogIn userModel)
        {
            using(var repoFactory = UnitOfWorkManager.GetRepositoryFactory())
            {
                var _userRepo = repoFactory.GetRepository<User>();

                UserModel? userEntity = await _userRepo.UnTrackableQuery()
                    .Where(user => user.Email == userModel.Email)
                    .Select(user => new UserModel
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Password = user.Password,
                    })
                    .FirstOrDefaultAsync();

                if(userEntity == null)
                    throw new ArgumentException("No user registered with this email");

                if (ReferenceEquals(userEntity.Password, userModel.Password))
                {
                    return GenerateJwtToken(new List<Claim> {
                        new Claim("UserId", userEntity.UserId.ToString()),
                        new Claim("UserName", userEntity.UserName),
                    });
                }
                else
                    throw new ArgumentException("Invalid password");
            }
        }

        public Task ResetPassword()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> SignUp(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateInfoAsync(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        private string GenerateJwtToken(List<Claim> claimList)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = new JwtSecurityToken
                (
                    issuer: "BlueprintX",
                    audience: "User",
                    expires: DateTime.UtcNow.AddDays(CommonConstants.PasswordConfig.SaltExpire),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(CommonConstants.PasswordConfig.SaltByte),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                    claims: claimList
                );

            return tokenHandler.WriteToken(token);
        }

    }
}
