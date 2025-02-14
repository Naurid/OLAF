namespace DisneyBattle.WebAPI.Models
{
    public class EquipeModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int  UtilisateurId{ get; set; }
        public DateTime DateCreation { get; set; }

        public List<TempPersonnageModel> Personnages { get; set; } = new List<TempPersonnageModel>();
    }
}
