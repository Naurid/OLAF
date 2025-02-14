namespace DisneyBattle.WebAPI.Models;


public class UtilisateurInModel(string pseudo,string email,string motDePasse)
{
    public string Pseudo {get;set;} = pseudo;
    public string Email {get;set;} = email;
    public string MotDePasse {get;set;} = motDePasse;
}