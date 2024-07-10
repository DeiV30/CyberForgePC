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

    public class User : IUser
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAuthenticationHelper authenticationHelper;
        private readonly ISecurityHelper passwordHasher;
        private readonly IMailHelper emailServices;

        public User(IUnitOfWork unitOfWork, IAuthenticationHelper authenticationHelper,
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
            var user = await unitOfWork.Users.FindWhere(u => u.Email == request.Email);

            if (user == null)
                throw new UserNotExistsException("No encontramos registro de este correo en nuestro sistema");



            if (user.PasswordHash != null)
            {
                bool validUser = false;

                validUser = user.PasswordHash.SequenceEqual(passwordHasher.HashPassword(request.Password, user.PasswordSalt).PasswordHash);

                if (validUser == false)
                    throw new UserPasswordIncorrectException("Verifica los datos de ingrerso");

                var accessToken = authenticationHelper.Authenticate(user);
                string refreshToken = authenticationHelper.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTimeStamp = accessToken.Expired;

                var userData = new UserResponseAuth
                {
                    RefreshToken = refreshToken,
                    AccessToken = accessToken.Token,
                };

                unitOfWork.Users.Update(user);
                await unitOfWork.Save();

                return userData;
            }
            else
            {
                throw new UserNotPasswordException("Dirigete a recuperar mi contraseña y asigna una nueva");
            }
        }
        public async Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var principal = authenticationHelper.GetPrincipalFromExpiredToken(request.AccessToken);

            if (principal == null)
                throw new UserSecurityTokenException("Invalid token");

            var username = principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First();
            var user = await unitOfWork.Users.FindWhere(u => u.Id == username.Value);

            if (user.RefreshToken != request.RefreshToken)
                throw new UserSecurityTokenException("Invalid refresh token");

            var newToken = authenticationHelper.GenerateToken(principal.Claims);
            var newRefreshToken = authenticationHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTimeStamp = newToken.Expired;

            unitOfWork.Users.Update(user);
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
            var users = await unitOfWork.Users.GetWhere(u => u.Discriminator == "Client");

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
            var user = await unitOfWork.Users.FindWhere(u => u.Id.Equals(id));

            if (user == null)
                throw new UserNotExistsException("User not found in the database.");

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
            var userExists = await unitOfWork.Users.FindWhere(u => u.Email.Equals(request.Email));

            if (userExists != null)
                throw new UserAlreadyExistsException("Este correo ya se encuentra registrado en nuestro sistema");

            var passwordHashResult = passwordHasher.HashPassword(request.Password);

            var id = Guid.NewGuid().ToString();

            var userToCreate = new Users
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

            unitOfWork.Users.Add(userToCreate);

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
            var userToUpdate = await unitOfWork.Users.FindWhere(u => u.Id.Equals(id));

            if (userToUpdate == null)
                throw new UserNotExistsException("User not found in the database.");

            userToUpdate.Name = request.Name;
            userToUpdate.Email = request.Email;

            userToUpdate.Updated = DateTime.UtcNow;

            if (request.Password != null)
            {
                var passwordHashResult = passwordHasher.HashPassword(request.Password);
                userToUpdate.PasswordHash = passwordHashResult.PasswordHash;
                userToUpdate.PasswordSalt = passwordHashResult.PasswordSalt;
            }

            unitOfWork.Users.Update(userToUpdate);
            await unitOfWork.Save();

            return true;
        }
        #endregion
    }
}
