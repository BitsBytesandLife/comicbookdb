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
        public List<comicDetails> ShowDetails()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT comicBookDetailID, comicBookID, detail " +
                                  "FROM comicdetails; ";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<comicDetails> cbDetails = new List<comicDetails>();
                while (reader.Read())
                {
                    comicDetails details = new comicDetails();
                    details.comicBookDetailID = (int)reader["comicBookDetailID"];
                    details.comicBookID = (int)reader["comicBookID"];
                    details.detail = (string)reader["detail"];
                    cbDetails.Add(details);

                    Console.WriteLine($"Comic Book Detail ID: {details.comicBookDetailID} Comic Book ID:{details.comicBookID} Detail:{details.detail}"); 
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

                cmd.CommandText = "INSERT INTO comicDetails (comicBookDetailID, comicBookID ,detail) " +
                                   "VALUES (@comicBookDetailID, @comicBookID ,@detail)";
                cmd.Parameters.AddWithValue("comicBookDetailID", cbd.comicBookDetailID);
                cmd.Parameters.AddWithValue("comicBookID", cbd.comicBookID);
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

                cmd.CommandText = "UPDATE comicDetails SET detail = @detail " +
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