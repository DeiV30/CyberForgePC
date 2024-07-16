namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Authentication;
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Helpers.Mail;
    using cyberforgepc.Helpers.Security;
    using cyberforgepc.Models.Authentication;
    using cyberforgepc.Models.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class Users : IUsers
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ISecurityHelper passwordHasher;
        private readonly IMailHelper emailServices;

        public Users(IUnitOfWork unitOfWork, IAuthenticationHelper authenticationHelper,
            ISecurityHelper passwordHasher, IMailHelper emailServices)
        {
            this.unitOfWork = unitOfWork;
            this.authenticationHelper = authenticationHelper;
            this.passwordHasher = passwordHasher;
            this.emailServices = emailServices;
        }

        #region "Authentication"

        public async Task<UserResponseAuth> Authenticate(UserLoginRequest request)
        {
            var user = await unitOfWork.User.FindWhere(u => u.Email == request.Email);

            if (user == null)
                throw new MessageException("No se han encontrado resultados.");



            if (user.PasswordHash != null)
            {
                bool validUser = false;

                validUser = user.PasswordHash.SequenceEqual(passwordHasher.HashPassword(request.Password, user.PasswordSalt).PasswordHash);

                if (validUser == false)
                    throw new MessageException("Verifica los datos de ingrerso");

                var accessToken = authenticationHelper.Authenticate(user);
                string refreshToken = authenticationHelper.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTimeStamp = accessToken.Expired;

                var userData = new UserResponseAuth
                {
                    RefreshToken = refreshToken,
                    AccessToken = accessToken.Token,
                };

                unitOfWork.User.Update(user);
                await unitOfWork.Save();

                return userData;
            }
            else
            {
                throw new MessageException("Dirigete a recuperar mi contraseña y asigna una nueva");
            }
        }
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = authenticationHelper.GetPrincipalFromExpiredToken(request.AccessToken);

            if (principal == null)
                throw new MessageException("Token invalido");

            var username = principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First();
            var user = await unitOfWork.User.FindWhere(u => u.Id == username.Value);

            if (user.RefreshToken != request.RefreshToken)
                throw new MessageException("Token invalido de reautenticacion");

            var newToken = authenticationHelper.GenerateToken(principal.Claims);
            var newRefreshToken = authenticationHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTimeStamp = newToken.Expired;

            unitOfWork.User.Update(user);
            await unitOfWork.Save();

            return new RefreshTokenResponse
            {
                NewAccessToken = newToken.Token,
                NewRefreshToken = newRefreshToken
            };
        }

        #endregion

        #region "CRUD"

        public async Task<List<UserResponse>> GetAll()
        {
            var users = await unitOfWork.User.GetWhere(u => u.Discriminator == "Client");

            var usersResponse = new List<UserResponse>();

            users.OrderByDescending(us => us.Created).ToList().ForEach(u =>
            {
                usersResponse.Add(new UserResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Created = u.Created,
                    Updated = u.Updated
                });
            });

            return usersResponse;
        }

        public async Task<UserResponse> GetById(string id)
        {
            var user = await unitOfWork.User.FindWhere(u => u.Id.Equals(id));

            if (user == null)
                throw new MessageException("No se han encontrado resultados.");

            UserResponse userResponse = new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Created = user.Created,
                Updated = user.Updated
            };

            return userResponse;
        }

        public async Task<bool> Create(UserInsertRequest request)
        {
            var userExists = await unitOfWork.User.FindWhere(u => u.Email.Equals(request.Email));

            if (userExists != null)
                throw new MessageException("No se han encontrado resultados.");

            var passwordHashResult = passwordHasher.HashPassword(request.Password);

            var id = Guid.NewGuid().ToString();

            var userToCreate = new User
            {
                Id = id,
                Name = request.Name,
                Email = request.Email,
                Discriminator = Role.Client,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                PasswordHash = passwordHashResult.PasswordHash,
                PasswordSalt = passwordHashResult.PasswordSalt
            };

            unitOfWork.User.Add(userToCreate);

            await unitOfWork.Save();

            //var data = new Dictionary<string, object> {
            //        { "Id", userToCreate.Id },
            //        { "Name", userToCreate.Name },
            //        { "ActiveToken", userToCreate.ActiveToken },
            //    };

            //emailServices.PostmarkAsync(postmarkSettings.Template.Activate, userToCreate.Email, data).Wait();

            return true;
        }

        public async Task<bool> Update(string id, UserUpdateRequest request)
        {
            var userToUpdate = await unitOfWork.User.FindWhere(u => u.Id.Equals(id));

            if (userToUpdate == null)
                throw new MessageException("No se han encontrado resultados.");

            userToUpdate.Name = request.Name;
            userToUpdate.Email = request.Email;

            userToUpdate.Updated = DateTime.UtcNow;

            if (request.Password != null)
            {
                var passwordHashResult = passwordHasher.HashPassword(request.Password);
                userToUpdate.PasswordHash = passwordHashResult.PasswordHash;
                userToUpdate.PasswordSalt = passwordHashResult.PasswordSalt;
            }

            unitOfWork.User.Update(userToUpdate);
            await unitOfWork.Save();

            return true;
        }
        #endregion
    }
}
