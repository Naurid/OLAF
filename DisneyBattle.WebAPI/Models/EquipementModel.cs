namespace DisneyBattle.WebAPI.Models
{
    public class EquipementModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public int NiveauRequis {  get; set; }
        public int BonusPV { get; set; }
        public int BonusAttaque { get; set; }
        public int BonusDefense { get; set; }
    }
}
