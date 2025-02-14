namespace DisneyBattle.WebAPI.Models;


public class UtilisateurModel(int id,string pseudo,string email,string mot_de_passe, DateTime date_inscription)
{
    public int Id {get;set;} = id;
    public string Pseudo {get;set;} = pseudo;
    public string Email {get;set;} = email;
    public string MotDePasse {get;set;} = mot_de_passe;
    public DateTime DateInscription {get;set;} = date_inscription;
}