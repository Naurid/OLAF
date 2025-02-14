namespace DisneyBattle.WebAPI.Models
{
    public class TempUtilisateurModel
    {
        public int Id { get; set; }
        public string Login { get; set; }

        public TempUtilisateurModel Joueur1 = new TempUtilisateurModel { Id = 1, Login = "Yashidao" };
        public TempUtilisateurModel Joueur2 = new TempUtilisateurModel { Id = 2, Login = "Lafther" };
    }

}
