public class LieuModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    
    public LieuModel()
    {
    }

    public LieuModel(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
