using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ComicDatabaseProject
{
    class publisherRepository
    {
        private static string connectionString;

        public publisherRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }


        /// <summary>
        ///  Shows the publisher table. 
        /// </summary>
        public List<publisher> GetPublishers()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT publisherID,publisherName " +
                                  "FROM publisher;";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<publisher> cbp = new List<publisher>();
                while (reader.Read())
                {
                    publisher p = new publisher();

                    p.publisherName = (string)reader["publisherName"];

                    cbp.Add(p);
                }

                return cbp;
            }
        }

        /// <summary>
        ///     Creates record in the publisher table.
        /// </summary>
        public void CreateComicPublisherRecord(publisher cbp)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO publisher (publisherName) " +
                                   "VALUES (@publisherName)";
                cmd.Parameters.AddWithValue("publisherName", cbp.publisherName);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Updates the field "publisherName"
        /// </summary>
        public void UpdateComicPubliherRecord(publisher cbp)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE publisher SET publisherName = @publisherName " +
                                  "WHERE publisherID = @publisherID";

                cmd.Parameters.AddWithValue("publisherID", cbp.publisherID);
                cmd.Parameters.AddWithValue("publisherName", cbp.publisherName);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes record in the condition table.
        /// </summary>
        public void DeleteComicPublisherIDRecord(publisher cbp)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM publisher WHERE publisherID = @publisherID;";
                cmd.Parameters.AddWithValue("publisherID", cbp);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteComicDetailRecord(int cbp)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM publisher WHERE publisherID = @publisherID;";
                cmd.Parameters.AddWithValue("publisherID", cbp);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
