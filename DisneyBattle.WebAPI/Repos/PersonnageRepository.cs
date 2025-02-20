using Microsoft.Data.SqlClient;
using DisneyBattle.WebAPI.Models;
using System.Collections.Generic;

namespace DisneyBattle.WebAPI.Repositories
{
    public class PersonnageRepository : IPersonnageRepository
    {
        private readonly string _connectionString;

        public PersonnageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Insert(PersonnageModel personnage)
        {
            string query = "INSERT INTO personnages (nom, alignement_id, niveau, experience, points_vie, points_attaque, points_defense, lieu_id) " +
                           "VALUES (@Nom, @AlignementId, @Niveau, @Experience, @PointsVie, @PointsAttaque, @PointsDefense, @LieuId)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nom", personnage.Nom);
                cmd.Parameters.AddWithValue("@AlignementId", personnage.AlignementId);
                cmd.Parameters.AddWithValue("@Niveau", personnage.Niveau);
                cmd.Parameters.AddWithValue("@Experience", personnage.Experience);
                cmd.Parameters.AddWithValue("@PointsVie", personnage.PointsVie);
                cmd.Parameters.AddWithValue("@PointsAttaque", personnage.PointsAttaque);
                cmd.Parameters.AddWithValue("@PointsDefense", personnage.PointsDefense);
                cmd.Parameters.AddWithValue("@LieuId", personnage.LieuId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Update(int id, PersonnageModel personnage)
        {
            string query = "UPDATE personnages SET nom = @Nom, alignement_id = @AlignementId, niveau = @Niveau, " +
                           "experience = @Experience, points_vie = @PointsVie, points_attaque = @PointsAttaque, " +
                           "points_defense = @PointsDefense, lieu_id = @LieuId WHERE id = @Id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Nom", personnage.Nom);
                cmd.Parameters.AddWithValue("@AlignementId", personnage.AlignementId);
                cmd.Parameters.AddWithValue("@Niveau", personnage.Niveau);
                cmd.Parameters.AddWithValue("@Experience", personnage.Experience);
                cmd.Parameters.AddWithValue("@PointsVie", personnage.PointsVie);
                cmd.Parameters.AddWithValue("@PointsAttaque", personnage.PointsAttaque);
                cmd.Parameters.AddWithValue("@PointsDefense", personnage.PointsDefense);
                cmd.Parameters.AddWithValue("@LieuId", personnage.LieuId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public PersonnageModel GetById(int id)
        {
            PersonnageModel personnage = null;
            string query = @"
                SELECT p.id, p.nom, p.alignement_id, p.niveau, p.experience, p.points_vie, p.points_attaque, p.points_defense, p.lieu_id,
                       l.id AS LieuId, l.nom AS LieuNom, l.description AS LieuDescription
                FROM personnages p
                LEFT JOIN lieux l ON p.lieu_id = l.id
                WHERE p.id = @id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        personnage = new PersonnageModel
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            AlignementId = reader.GetInt32(2),
                            Niveau = reader.GetInt32(3),
                            Experience = reader.GetInt32(4),
                            PointsVie = reader.GetInt32(5),
                            PointsAttaque = reader.GetInt32(6),
                            PointsDefense = reader.GetInt32(7),
                            LieuId = reader.GetInt32(8),
                            Lieu = new LieuModel
                            {
                                Id = reader.GetInt32(9),
                                Nom = reader.GetString(10),
                                Description = reader.GetString(11)
                            }
                        };
                    }
                }
            }
            return personnage;
        }

        public IEnumerable<PersonnageModel> GetAll()
        {
            var personnages = new List<PersonnageModel>();
            string query = @"
                SELECT p.id, p.nom, p.alignement_id, p.niveau, p.experience, p.points_vie, p.points_attaque, p.points_defense, p.lieu_id,
                       l.id AS LieuId, l.nom AS LieuNom, l.description AS LieuDescription
                FROM personnages p
                LEFT JOIN lieux l ON p.lieu_id = l.id";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personnages.Add(new PersonnageModel
                        {
                            Id = reader.GetInt32(0),
                            Nom = reader.GetString(1),
                            AlignementId = reader.GetInt32(2),
                            Niveau = reader.GetInt32(3),
                            Experience = reader.GetInt32(4),
                            PointsVie = reader.GetInt32(5),
                            PointsAttaque = reader.GetInt32(6),
                            PointsDefense = reader.GetInt32(7),
                            LieuId = reader.GetInt32(8),
                            Lieu = new LieuModel
                            {
                                Id = reader.GetInt32(9),
                                Nom = reader.GetString(10),
                                Description = reader.GetString(11)
                            }
                        });
                    }
                }
            }
            return personnages;
        }
    }
}
