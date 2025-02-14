namespace DisneyBattle.WebAPI.Models
{
    public class CombatModel
    {
        public int Id { get; set; }
        public List<EquipeModel> Equipes { get; set; } = new List<EquipeModel>();
    }
}