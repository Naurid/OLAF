using DisneyBattle.WebAPI.Models;

namespace DisneyBattle.WebAPI.Services.IServices
{
    public interface IUserService : IService<UtilisateurModel>
    {
        UtilisateurModel? Authenticate(string username, string password);
        bool Checkrefresh(string access_Token, string refresh_Token);
    }
}
