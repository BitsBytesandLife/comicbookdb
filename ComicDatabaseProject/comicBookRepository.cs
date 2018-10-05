using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ComicDatabaseProject
{
    

    class comicBookRepository
    {
        private static string connectionString;

        public comicBookRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }

        /// <summary>
        ///  Shows the comicbook table. 
        ///  It shows the raw data of the table.
        ///  BK:Make another method that shows the  results from a the joined table.
        /// </summary>
        public List<comicbooks> GetComicbooks()
        { 
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT title, issue, publisher, comicBookCondition, comicDetail, comicBookValue /n" +
                                  "FROM comicbooks:";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<comicbooks> cb = new List<comicbooks>();
                while (reader.Read())
                {
                    comicbooks comic = new comicbooks();
                    comic.title = (string)reader["title"];
                    comic.issue = (int)reader["issue"];
                    comic.publisher = (int)reader["publisher"];
                    comic.comicBookCondition = (int)reader["comicBookCondition"];
                    comic.comicDetail = (int)reader["comicDetail"];
                    comic.comicBookValue = (int)reader["comicBookValue"];
                    cb.Add(comic);
                }
                return cb;
            }
        }

        /// <summary>
        ///     Creates record in the comicbooks table.
        /// </summary>
        public void CreateComicBookRecord(comicbooks cb)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO comicbooks (title, issue, publisher, comicBookCondition) " +
                                   "VALUES (@title, @issue,@publisher,@comicBookCondition)";
                cmd.Parameters.AddWithValue("title", cb.title);
                cmd.Parameters.AddWithValue("issue", cb.issue);
                cmd.Parameters.AddWithValue("publisher", cb.publisher);
                cmd.Parameters.AddWithValue("comicBookCondition", cb.comicBookCondition);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateComicBookAll(comicbooks cb)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicBook SET title = @title, issue = @issue, " +
                                  "publisher = @publisher,comicBookCondition =  @comicBookCondition " +
                                  "WHERE comicBookID = @comicBookID";
                cmd.Parameters.AddWithValue("comicBookID", cb.comicBookID);
                cmd.Parameters.AddWithValue("title", cb.title);
                cmd.Parameters.AddWithValue("issue", cb.issue);
                cmd.Parameters.AddWithValue("publisher", cb.publisher);
                cmd.Parameters.AddWithValue("comicBookCondition", cb.comicBookCondition);
                cmd.ExecuteNonQuery();
            }

        }


        /// <summary>
        /// This method field comicBookValue in the comicbooks table
        /// </summary>
        public void UpdateComicBookValue(comicbooks cb)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicBooks SET comicBookValue = @comicBookValue " +
                                  "WHERE comicBookID = @comicBookID";
                cmd.Parameters.AddWithValue("comicBookID", cb.comicBookID);
                cmd.Parameters.AddWithValue("comicBookValue", cb.comicBookValue);
                cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// This method field comicDetail in the comicbooks table
        /// </summary>
        public void UpdateComicDetail(comicbooks cb)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicBooks SET comicDetail = @comicDetail " +
                                  "WHERE comicBookID = @comicBookID";
                cmd.Parameters.AddWithValue("comicBookID", cb.comicBookID);
                cmd.Parameters.AddWithValue("comicDetail", cb.comicDetail);
                cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        ///     DeleteLocation method deletes and record from the comicbooks table
        ///     takes one parameters  (comicBookID)
        ///     Deletes and record with a specific comicBookID.
        /// </summary>
        public void DeleteComicBookRecord(comicbooks cb)

        {

            using (var conn = new MySqlConnection(connectionString))

            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicbooks WHERE comicBookID = @comicBookID;";
                cmd.Parameters.AddWithValue("comicBookID", cb.comicBookID);
                cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        ///     DeleteLocation method deletes and record from the comicbooks table
        ///     takes one parameters  (comicBookID)
        ///     Deletes and record with a specific comicBookID.
        /// </summary>
        public void DeleteComicBookRecord(int cbID)

        {

            using (var conn = new MySqlConnection(connectionString))

            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicbooks WHERE comicBookID = @comicBookID;";
                cmd.Parameters.AddWithValue("comicBookID", cbID);
                cmd.ExecuteNonQuery();
            }

        }
    }
}
