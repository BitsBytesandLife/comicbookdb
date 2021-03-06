﻿using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace ComicDatabaseProject
{
    class comicValueRepositiory
    {

        private static string connectionString;

        public comicValueRepositiory(string _connectionString)
        {
            connectionString = _connectionString;
        }


        /// <summary>
        ///  Shows the comicBookDetails table. 
        /// </summary>
        public List<comicValue> GetComicValues()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT comicBookID, orginalPrice, currentValue " +
                                  "FROM comicvalue;";
                MySqlDataReader reader = cmd.ExecuteReader();


                List<comicValue> cbValue = new List<comicValue>();
                while (reader.Read())
                {
                    comicValue cbv = new comicValue();
                    cbv.comicId = (int)reader["comicBookID"];
                    cbv.originalPrice = (decimal)reader["orginalPrice"];
                    cbv.currentValue = (decimal)reader["currentValue"];
                   
                    cbValue.Add(cbv);

                    Console.WriteLine($"Comic ID: {cbv.comicId} Original Price: {cbv.originalPrice} Current Price:{cbv.currentValue}");
                }

                return cbValue;
            }
        }


        /// <summary>
        ///     Creates record in the comicValues table.
        /// </summary>
        public void CreateComicValueRecord(comicValue cbv)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "INSERT INTO comicvalue (comicBookID, orginalPrice, currentValue) " +
                                   "VALUES (@comicBookID, @orginalPrice, @currentValue)";
                cmd.Parameters.AddWithValue("comicBookID", cbv.comicId);
                cmd.Parameters.AddWithValue("orginalPrice", cbv.originalPrice);
                cmd.Parameters.AddWithValue("currentValue", cbv.currentValue);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Updates the field "currentValue"
        /// </summary>
        public void UpdateComicValueRecord(comicValue cbv)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "UPDATE comicvalue SET currentValue = @currentValue " +
                                  "WHERE comicVauleID = @comicValueID";

                cmd.Parameters.AddWithValue("comicValueID", cbv.comicValueID);
                cmd.Parameters.AddWithValue("currentValue",cbv.currentValue);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes record in the comicvalues table.
        /// </summary>
        public void DeleteComicValuesRecord(comicValue cbv)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicbookdb.comicvalue WHERE comicvalue.comicValueID = @comicValueID;";
                cmd.Parameters.AddWithValue("comicValueID", cbv.comicValueID);
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteComicValueRecord(int cbv)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "DELETE FROM comicvalue WHERE comicValueID = @comicValueID;";
                cmd.Parameters.AddWithValue("comicvalueID", cbv);
                cmd.ExecuteNonQuery();
            }
        }



    }
}
