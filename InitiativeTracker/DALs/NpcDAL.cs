using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitiativeTracker.Models;

namespace InitiativeTracker.DALs
{
    public class NpcDAL
    {
        //Member Variables
        private string _connectionString;

        //Single Parameter Constructor

        /// <summary>
        /// Constructor!
        /// </summary>
        /// <param name="dbConnection">Connection String for SQL DataBase</param>
        public NpcDAL(string dbConnection)
        {
            _connectionString = dbConnection;
        }

        public List<NonPlayerCharacter> GetEnemiesByType(string type)
        {
            List<NonPlayerCharacter> result = new List<NonPlayerCharacter>();

            //Connect to Database
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                //Create sql statement
                string sqlPlayer = "SELECT  name, type, CR, initiative_bonus, AC, description, race " +
                                       "FROM npc " +
                                       $"WHERE type LIKE @pc_id ;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlPlayer;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@pc_id", ("%"+type+"%"));
                //Send command to database
                SqlDataReader reader = cmd.ExecuteReader();

                //Pull data off of result set
                while (reader.Read())
                {
                    NonPlayerCharacter enemy = new NonPlayerCharacter();

                    enemy.Name = Convert.ToString(reader["name"]);
                    enemy.InitiativeBonus = Convert.ToInt32(reader["initiative_bonus"]);
                    enemy.ArmorClass = Convert.ToInt32(reader["AC"]);
                    enemy.Description = Convert.ToString(reader["description"]);

                    enemy.TypeClass = Convert.ToString(reader["type"]);
                    enemy.Level = Convert.ToInt32(reader["CR"]);
                    enemy.Race = Convert.ToString(reader["race"]);

                    result.Add(enemy);
                }
            }


            return result;
        }
    }
}
