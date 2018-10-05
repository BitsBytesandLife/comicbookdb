using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ComicDatabaseProject
{

    class comicDetailsRepository
    {
        private static string connectionString;

        public comicDetailsRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }


        /// <summary>
        ///  Shows the comicBookDetails table. 
        /// </summary>
        public List<comicDetails> GetDetails()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT comicBookDetailID, detail " +
                                  "FROM comicdetails; ";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<comicDetails> cbDetails = new List<comicDetails>();
                while (reader.Read())
                {
                    comicDetails details = new comicDetails();
                    details.comicBookDetailID = (int)reader["comicBookDetailID"];
                    details.detail = (string)reader["details"];
                    cbDetails.Add(details);
                }

                return cbDetails;
            }
        }


        /// <summary>
        ///     Creates record in the comicDetails table.
        /// </summary>
        public void CreateComicDetailRecord(comicDetails cbd)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO comicDetails (comicBookDetailID, detail) " +
                                   "VALUES (@comicBookDetailID, @detail)";
                cmd.Parameters.AddWithValue("comicBookDetailID", cbd.comicBookDetailID );
                cmd.Parameters.AddWithValue("detail", cbd.detail);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Updates the field "details"
        /// </summary>
        public void UpdateComicDetailRecord(comicDetails cbd)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicDetails SET detail = @deatil " +
                                  "WHERE comicBookDetailID = @comicBookDetailID";

                cmd.Parameters.AddWithValue("comicBookDetailID", cbd.comicBookDetailID);
                cmd.Parameters.AddWithValue("detail", cbd.detail);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes record in the comicDetails table.
        /// </summary>
        public void DeleteComicDetailRecord(comicDetails cbd)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicDetails WHERE comicBookDetailID = @comicBookDetailID;";
                cmd.Parameters.AddWithValue("comicBookDetailID", cbd.comicBookDetailID);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteComicDetailRecord(int cbd)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicDetails WHERE comicBookDetailID = @comicBookDetailID;";
                cmd.Parameters.AddWithValue("comicBookDetailID", cbd);
                cmd.ExecuteNonQuery();
            }
        }
    }
}