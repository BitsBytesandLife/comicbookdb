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
        /// </summary>
        public List<comicbooks> GetComicbooks()
        { 
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT title, issue, publisher, comicBookCondition " +
                                  "FROM comicbooks;";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<comicbooks> cb = new List<comicbooks>();
                while (reader.Read())
                {
                    comicbooks comic = new comicbooks();
                    comic.title = (string)reader["title"];
                    comic.issue = (int)reader["issue"];
                    comic.publisher = (string)reader["publisher"];
                    comic.comicBookCondition = (string)reader["comicBookCondition"];
                    cb.Add(comic);

                    Console.WriteLine($"Title: {comic.title} Issue:{comic.issue} Publisher:{comic.publisher} \n" +
                                      $"Condition: {comic.comicBookCondition}");
                }
                return cb;
            }
        }

        /// <summary>
        ///     Creates record in the comicbooks table.
        /// </summary>
        public void CreateCBRecord(comicbooks cb)
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

                cmd.CommandText = "UPDATE comicbooks SET title = @title, issue = @issue, " +
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
        public void UpdateCBCondition(string cbd,int cbi)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicBooks SET comicBookCondition = @comicBookCondition " +
                                  "WHERE comicBookID = @comicBookID";
                cmd.Parameters.AddWithValue("comicBookID", cbi);
                cmd.Parameters.AddWithValue("comicBookValue", cbd);
                cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// This method field comicDetail in the comicbooks table
        /// </summary>
        public void UpdateCBCondition(comicbooks cb)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicbooks SET comicBookCondition = @comicBookCondition " +
                                  "WHERE comicBookID = @comicBookID";
                cmd.Parameters.AddWithValue("comicBookID", cb.comicBookID);
                cmd.Parameters.AddWithValue("comicBookCondition", cb.comicBookCondition);
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
            var conn = new MySqlConnection(connectionString);

            using(conn)

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
            var conn = new MySqlConnection(connectionString);

            using(conn)

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
