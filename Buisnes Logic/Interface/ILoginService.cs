using System.IdentityModel.Tokens.Jwt;

namespace Buisnes_Logic.Interface
{
    public interface ILoginService
    {
        string Login(string username, string password);
    }
}
