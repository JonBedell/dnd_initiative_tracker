using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker.Models;
using System.Data.SqlClient;

namespace InitiativeTracker.DALs
{
    public class PlayerDAL
    {
        //Member Variables
        private string _connectionString;

        //Single Parameter Constructor

        /// <summary>
        /// Constructor!
        /// </summary>
        /// <param name="dbConnection">Connection String for SQL DataBase</param>
        public PlayerDAL(string dbConnection)
        {
            _connectionString = dbConnection;
        }

        /// <summary>
        /// Get all players in Database
        /// </summary>
        /// <returns>List of all players</returns>
        public List<Player> GetAllPlayers()
        {
            List<Player> result = new List<Player>();

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlPlayer = "SELECT name, player_id " +
                                       "FROM player ";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlPlayer;
                cmd.Connection = conn;

                //Send command to database
                SqlDataReader reader = cmd.ExecuteReader();

                //Pull data off of result set
                while (reader.Read())
                {
                    Player player = new Player();

                    player.PlayerID = Convert.ToInt32(reader["player_id"]);
                    player.Name = Convert.ToString(reader["name"]);
                    result.Add(player);
                }
            }
            return result;
        }


        /// <summary>
        /// Grab a player based on player ID.
        /// </summary>
        /// <returns>Player object based on ID</returns>
        public Player GetPlayerByID(int id)
        {
            Player result = new Player();

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlPlayer = "SELECT name, player_id " +
                                       "FROM player " +
                                       $"WHERE player_id = @player_id ";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlPlayer;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@player_id", id);
                //Send command to database
                SqlDataReader reader = cmd.ExecuteReader();

                //Pull data off of result set
                while (reader.Read())
                {
                    Player player = new Player();

                    player.PlayerID = Convert.ToInt32(reader["player_id"]);
                    player.Name = Convert.ToString(reader["name"]);
                    result = player;
                }
            }
            return result;
        }

        public void AddPlayer(string name)
        {

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlReservation = "INSERT INTO player (name) " +
                                        $"VALUES(@name)";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlReservation;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@name", name);

                //Send command to database
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("DATABASE ERROR: Player Could not be added.");
                }
            }
        }
    }
}