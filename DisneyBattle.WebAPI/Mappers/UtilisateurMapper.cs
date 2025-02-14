

using System.Data;
using DisneyBattle.WebAPI.Models;
namespace DisneyBattle.WebAPI.Mappers;

public partial class Mappers
{
    public static UtilisateurModel ToUtilisateur(this IDataRecord record)
    {
        return new UtilisateurModel((int)record["id"],(string)record["pseudo"],(string)record["email"],(string)record["mot_de_passe"],(DateTime)record["date_inscription"]);
    }
}