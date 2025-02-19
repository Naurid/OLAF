namespace DinseyBattle.Blazor.Models;

public class PointFaibleModel
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public PointFaibleModel(){}

    public PointFaibleModel(int id, string name, string description)
    {
        Id = id;
        Nom = name;
        Description = description;
    }
}