using System.Data;
using DisneyBattle.WebAPI.Models;

namespace DisneyBattle.WebAPI.Mappers;

public static partial class Mappers
{
    public static LieuModel ToLieu(this IDataRecord record)
    {
        return new LieuModel((int)record["id"], (string)record["nom"], (string)record["description"]);
    }
}