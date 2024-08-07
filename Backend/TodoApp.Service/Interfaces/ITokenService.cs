using TodoApp.Service.DTO;

namespace TodoApp.Service.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserInfo user);
    }
}
