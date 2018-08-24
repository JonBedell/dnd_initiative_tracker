using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker.Models;

namespace InitiativeTracker.DALs
{
    public class PcDAL
    {
        //Member Variables
        private string _connectionString;

        //Single Parameter Constructor

        /// <summary>
        /// Constructor!
        /// </summary>
        /// <param name="dbConnection">Connection String for SQL DataBase</param>
        public PcDAL(string dbConnection)
        {
            _connectionString = dbConnection;
        }

        public List<PlayerCharacter> GetAllPlayersPCs(int playerID)
        {
            List<PlayerCharacter> result = new List<PlayerCharacter>();

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlPlayer = "SELECT  name, class, level, initiative_bonus, AC, description, player_id, pc_id " +
                                       "FROM pc " +
                                       $"WHERE player_id = @player_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlPlayer;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@player_id", playerID);
                //Send command to database
                SqlDataReader reader = cmd.ExecuteReader();

                //Pull data off of result set
                while (reader.Read())
                {
                    PlayerCharacter hero = new PlayerCharacter();

                    hero.Name = Convert.ToString(reader["name"]);
                    hero.InitiativeBonus = Convert.ToInt32(reader["initiative_bonus"]);
                    hero.ArmorClass = Convert.ToInt32(reader["AC"]);
                    hero.Description = Convert.ToString(reader["description"]);

                    hero.PlayerID = Convert.ToInt32(reader["player_id"]);
                    hero.PcId = Convert.ToInt32(reader["pc_id"]);
                    hero.TypeClass = Convert.ToString(reader["class"]);
                    hero.Level = Convert.ToInt32(reader["level"]);

                    result.Add(hero);
                }
            }
            return result;
        }

        public PlayerCharacter GetPcByID(int id)
        {
            PlayerCharacter result = new PlayerCharacter();

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlPlayer = "SELECT  name, class, level, initiative_bonus, AC, description, race, " +
                                       "FROM pc " +
                                       $"WHERE pc_id = @pc_id ";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlPlayer;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pc_id", id);
                //Send command to database
                SqlDataReader reader = cmd.ExecuteReader();

                //Pull data off of result set
                while (reader.Read())
                {
                    PlayerCharacter hero = new PlayerCharacter();

                    hero.Name = Convert.ToString(reader["name"]);
                    hero.InitiativeBonus = Convert.ToInt32(reader["initiative_bonus"]);
                    hero.ArmorClass = Convert.ToInt32(reader["AC"]);
                    hero.Description = Convert.ToString(reader["description"]);

                    hero.PlayerID = Convert.ToInt32(reader["player_id"]);
                    hero.TypeClass = Convert.ToString(reader["class"]);
                    hero.Level = Convert.ToInt32(reader["level"]);
                    hero.Race = Convert.ToString(reader["race"]);

                    result = hero;
                }
            }
            return result;
        }

        /// <summary>
        /// Add PC to database, WILL NEED PLAYER ID
        /// </summary>
        /// <param name="playerID">Primary key for character's player in database</param>
        /// <param name="name">Character Name</param>
        /// <param name="typeClass">Character Class</param>
        /// <param name="level">Char lvl</param>
        /// <param name="initiativeBonus">Dex Modifier + any other equipment modifiers</param>
        /// <param name="AC">Armor Class</param>
        /// <param name="race">Race</param>
        /// <param name="description">Short Character summary</param>
        public void AddPC(int playerID, string name, string typeClass, int level, int initiativeBonus, int AC, string race, string description)
        {

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlReservation = "INSERT INTO pc (name, player_id, class, level, initiative_bonus, AC, race, description) " +
                                        $"VALUES(@name, @player_id, @class, @level, @initiative_bonus, @AC, @race, @description)";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlReservation;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@player_id", playerID);
                cmd.Parameters.AddWithValue("@class", typeClass);
                cmd.Parameters.AddWithValue("@level", level);
                cmd.Parameters.AddWithValue("@initiative_bonus", initiativeBonus);
                cmd.Parameters.AddWithValue("@AC", AC);
                cmd.Parameters.AddWithValue("@race", race);
                cmd.Parameters.AddWithValue("@description", description);

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
