using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ComicDatabaseProject
{
    class condtionRepository
    {
        private static string connectionString;

        public condtionRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }


        /// <summary>
        ///  Shows the condition table. 
        /// </summary>
        public List<condition> GetComicValues()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT condtionID,comicBookCondtion " +
                                  "FROM `condition';";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<condition> cbc = new List<condition>();
                while (reader.Read())
                {
                    condition c = new condition();

                    c.conditionName = (string)reader["conditionName"];
                    

                    cbc.Add(c);
                }

                return cbc;
            }
        }

        /// <summary>
        ///     Creates record in the condition table.
        /// </summary>
        public void CreateComicConditionRecord(condition cbc)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO 'condition' (comicBookCondition) " +
                                   "VALUES (@comicBookCondition)";
                cmd.Parameters.AddWithValue("comicBookID", cbc.conditionName);
                
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Updates the field "conditionName"
        /// </summary>
        public void UpdateComicBookConditionRecord(condition cbc)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE 'condition' SET conditionName = @conditionName " +
                                  "WHERE conditionID = @conditionID";

                cmd.Parameters.AddWithValue("conditionID", cbc.conditionID);
                cmd.Parameters.AddWithValue("conditionName", cbc.conditionName);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes record in the condition table.
        /// </summary>
        public void DeleteComiConditionRecord(condition cbc)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM 'condition' WHERE conditionID = @conditionID;";
                cmd.Parameters.AddWithValue("conditionID", cbc.conditionID);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteComicDetailRecord(int cbc)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM 'condition' WHERE conditionID = @conditionID;";
                cmd.Parameters.AddWithValue("conditionID", cbc);
                cmd.ExecuteNonQuery();
            }
        }








    }
}
