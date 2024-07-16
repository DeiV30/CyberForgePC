namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Authentication;
    using cyberforgepc.Models.User;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsers
    {
        Task<UserResponseAuth> Authenticate(UserLoginRequest request);        
        Task<RefreshTokenResponse> RefreshToken(RefreshTokenRequest request);     
        Task<List<UserResponse>> GetAll();
        Task<UserResponse> GetById(string id);        
        Task<bool> Create(UserInsertRequest request);
        Task<bool> Update(string id, UserUpdateRequest request);        
    }
}
