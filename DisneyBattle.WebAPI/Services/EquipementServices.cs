using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;
using System.Data.Common;
using System.Data;

namespace DisneyBattle.WebAPI.Services
{
    public class EquipementServices : IEquipementServices
    {
        private readonly DbConnection _dbconnection;

        public EquipementServices(DbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        // Récupérer tous les équipements:
        public IEnumerable<EquipementModel> GetAll()
        {
            List<EquipementModel> equipements = new List<EquipementModel>();

            try
            {
                _dbconnection.Open();

                using (DbCommand command = _dbconnection.CreateCommand())
                {
                    command.CommandText = "SELECT id, nom, description, niveau_requis, bonus_pv, bonus_attaque, bonus_defense FROM dbo.objets";
                    command.CommandType = CommandType.Text;

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipements.Add(new EquipementModel
                            {
                                Id = reader.GetInt32("id"),
                                Nom = reader.GetString("nom"),
                                Description = reader.GetString("description"),
                                NiveauRequis = reader.GetInt32("niveau_requis"),
                                BonusPV = reader.GetInt32("bonus_pv"),
                                BonusAttaque = reader.GetInt32("bonus_attaque"),
                                BonusDefense = reader.GetInt32("bonus_defense")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log des erreurs ici:
                throw new Exception("Erreur lors de la récupération des équipements : " + ex.Message);
            }
            finally 
            {
                if (_dbconnection.State == ConnectionState.Open)
                {
                    _dbconnection.Close(); 
                }
            }
            return equipements;
        }

        // Récupérer un équipement par son ID
        public EquipementModel GetById(int id)
        {
            EquipementModel equipement = null;

            try
            {
                _dbconnection.Open();

                using (DbCommand command = _dbconnection.CreateCommand())
                {
                    command.CommandText = "SELECT id, nom, description, niveau_requis, bonus_pv, bonus_attaque, bonus_defense FROM dbo.objets WHERE id = @id";
                    command.CommandType = CommandType.Text;

                    DbParameter param = command.CreateParameter();
                    param.ParameterName = "@id";
                    param.Value = id;
                    param.DbType = DbType.Int32;
                    command.Parameters.Add(param);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            equipement = new EquipementModel
                            {
                                Id = reader.GetInt32(0),
                                Nom = reader.GetString(1),
                                Description = reader.GetString(2),
                                NiveauRequis = reader.GetInt32(3),
                                BonusPV = reader.GetInt32(4),
                                BonusAttaque = reader.GetInt32(5),
                                BonusDefense = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la récupération de l'équipement : " + ex.Message);
            }
            finally
            {
                if (_dbconnection.State == ConnectionState.Open)
                {
                    _dbconnection.Close();
                }
            }

            return equipement;
        }

        // Insérer un nouvel équipement
        public bool Insert(EquipementModel entity)
        {
            int rowsAffected = 0;

            try
            {
                _dbconnection.Open();

                using (DbCommand command = _dbconnection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO dbo.objets (nom, description, niveau_requis, bonus_pv, bonus_attaque, bonus_defense)
                        VALUES (@nom, @description, @niveau_requis, @bonus_pv, @bonus_attaque, @bonus_defense)";

                    command.CommandType = CommandType.Text;

                    command.Parameters.AddRange(new[]
                    {
                        CreateParameter(command, "@nom", entity.Nom, DbType.String),
                        CreateParameter(command, "@description", entity.Description, DbType.String),
                        CreateParameter(command, "@niveau_requis", entity.NiveauRequis, DbType.Int32),
                        CreateParameter(command, "@bonus_pv", entity.BonusPV, DbType.Int32),
                        CreateParameter(command, "@bonus_attaque", entity.BonusAttaque, DbType.Int32),
                        CreateParameter(command, "@bonus_defense", entity.BonusDefense, DbType.Int32)
                    });

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de l'insertion de l'équipement : " + ex.Message);
            }
            finally
            {
                if (_dbconnection.State == ConnectionState.Open)
                {
                    _dbconnection.Close();
                }
            }

            return rowsAffected > 0;
        }

        // Mettre à jour un équipement existant
        public bool Update(int id, EquipementModel entity)
        {
            int rowsAffected = 0;

            try
            {
                _dbconnection.Open();

                using (DbCommand command = _dbconnection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE dbo.objets 
                        SET nom = @nom, description = @description, niveau_requis = @niveau_requis, 
                            bonus_pv = @bonus_pv, bonus_attaque = @bonus_attaque, bonus_defense = @bonus_defense
                        WHERE id = @id";

                    command.CommandType = CommandType.Text;

                    command.Parameters.AddRange(new[]
                    {
                        CreateParameter(command, "@id", id, DbType.Int32),
                        CreateParameter(command, "@nom", entity.Nom, DbType.String),
                        CreateParameter(command, "@description", entity.Description, DbType.String),
                        CreateParameter(command, "@niveau_requis", entity.NiveauRequis, DbType.Int32),
                        CreateParameter(command, "@bonus_pv", entity.BonusPV, DbType.Int32),
                        CreateParameter(command, "@bonus_attaque", entity.BonusAttaque, DbType.Int32),
                        CreateParameter(command, "@bonus_defense", entity.BonusDefense, DbType.Int32)
                    });

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erreur lors de la mise à jour de l'équipement : " + ex.Message);
            }
            finally
            {
                if (_dbconnection.State == ConnectionState.Open)
                {
                    _dbconnection.Close();
                }
            }

            return rowsAffected > 0;
        }


        // Méthode utilitaire pour créer des paramètres SQL
        private DbParameter CreateParameter(DbCommand command, string name, object value, DbType type)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value ?? DBNull.Value;
            param.DbType = type;

            return param;
        }

    }
}
