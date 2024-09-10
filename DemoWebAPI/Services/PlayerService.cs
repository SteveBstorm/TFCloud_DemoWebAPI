using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace DemoWebAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly string _connectionString;

        public PlayerService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("default");
        }

        public Player Mapper(SqlDataReader reader)
        {
            return new Player
            {
                Id = (int)reader["Id"],
                Nickname = reader["Nickname"].ToString(),
                Email = reader["Email"].ToString(),
            };
        }

        public List<Player> GetAll()
        {
            List<Player> toReturn = new List<Player>();
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM player";
                    cnx.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            toReturn.Add(Mapper(reader));
                        }
                    }
                    cnx.Close();
                }
            }
            return toReturn;
        }

        public Player? GetById(int id)
        {
            Player player;
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM player WHERE Id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    try
                    {
                        cnx.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                player = Mapper(reader);
                            }
                            else
                            {
                                throw new NullReferenceException("Joueur introuvable");
                            }
                        }
                        cnx.Close();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }

                }
            }
            return player;
        }

        public bool Create(Player player)
        {
            bool resultat;
            string sql = "INSERT INTO player VALUES (@nickname, @email)";

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("nickname", player.Nickname);
                    cmd.Parameters.AddWithValue("email", player.Email);

                    cnx.Open();
                    resultat = cmd.ExecuteNonQuery() > 0;
                    cnx.Close();
                }
            }
            return resultat;
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM player WHERE Id = @id";

            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("id", id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }

        }

        public void Update(Player player)
        {
            using (SqlConnection cnx = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = cnx.CreateCommand())
                {
                    cmd.CommandText = "UPDATE player SET Nickname = @nick, Email = @mail " +
                        "WHERE Id = @id";

                    cmd.Parameters.AddWithValue("nick", player.Nickname);
                    cmd.Parameters.AddWithValue("mail", player.Email);
                    cmd.Parameters.AddWithValue("id", player.Id);

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    cnx.Close();
                }
            }
        }

    }
}
