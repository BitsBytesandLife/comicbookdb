using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;


namespace ComicDatabaseProject
{
    class DapperComicBookDBQueries
    {
        private static string connectionString;

        public DapperComicBookDBQueries(string _connectionString)
        {
            connectionString = _connectionString;
        }

        /// <summary>
        /// Dapper
        /// This method sets and runs the a query and returns the list from the query.
        /// SQL: SELECT c.title, c.issue, c.publisher, c.comicBookCondition, d.detail,v.currentValue " +
        ///      FROM comicbooks c 
        ///      INNER JOIN comicdetails d 
        ///         ON d.comicbookID = c.comicbookID 
        ///      INNER JOIN comicvalue v 
        ///         ON v.comicbookID = c.comicbookID;
        /// </summary>
        public List<ComicBookQueries> GetComicInfo()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<ComicBookQueries>("SELECT c.title, c.issue, c.publisher, c.comicBookCondition, d.detail,v.currentValue " +
                                              "FROM comicbooks c " +
                                              "INNER JOIN comicdetails d " +
                                              " ON d.comicbookID = c.comicbookID " +
                                              "INNER JOIN comicvalue v " +
                                              " ON v.comicbookID = c.comicbookID; ").ToList();
            }
        }

        
        /// <summary>
        /// Dapper
        /// This method sets and runs the a query and returns a single value and
        /// outputs results to the screen.
        /// SQL: SELECT sum(currentValue) as totalValue 
        ///      FROM comicvalue;
        /// </summary> 
        public decimal GetTotalValue()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<decimal>("SELECT sum(currentValue) as totalValue " +
                                                    "FROM comicvalue;").Single();
            }
        }

        /// <summary>
        /// Dapper
        /// This method sets and runs the a query and returns the list from the query
        /// it takes an parameter searchCriteria.
        /// SQL: SELECT c.title, c.issue, c.publisher, c.comicBookCondition, d.detail,v.currentValue " +
        ///      FROM comicbooks c 
        ///      INNER JOIN comicdetails d 
        ///         ON d.comicbookID = c.comicbookID 
        ///      INNER JOIN comicvalue v 
        ///         ON v.comicbookID = c.comicbookID
        ///      WHERE title like  @searchCriteria;;
        /// </summary>
        public List<ComicBookQueries> GetComicInfo(String searchCriteria)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<ComicBookQueries>("SELECT c.title, c.issue, c.publisher, c.comicBookCondition, d.detail,v.currentValue " +
                             "FROM comicbooks c " +
                             "INNER JOIN comicdetails d " +
                             " ON d.comicbookID = c.comicbookID " +
                             "INNER JOIN comicvalue v " +
                             " ON v.comicbookID = c.comicbookID " +
                             "WHERE title like  @searchCriteria;", new { searchCriteria = searchCriteria }).ToList();
            }
        }

    }
}
