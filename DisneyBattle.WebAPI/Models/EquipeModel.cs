namespace DisneyBattle.WebAPI.Models
{
    public class EquipeModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int  UtilisateurId{ get; set; }
        public DateTime DateCreation { get; set; }

        public EquipeModel(int id, string nom, int utilisateurId, DateTime dateCreation)
        {
            Id = id;
            Nom = nom;
            UtilisateurId = utilisateurId;
            DateCreation = dateCreation;
        }
    }
}
