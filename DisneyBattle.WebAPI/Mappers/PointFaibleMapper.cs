using System.Data;
using DisneyBattle.WebAPI.Models;

namespace DisneyBattle.WebAPI.Mappers;

public partial class Mappers
{
    public static PointFaibleModel ToPointFaible(this IDataRecord record)
    {
        return new PointFaibleModel((int)record["id"], (string)record["nom"], (string)record["description"]);
    }
}