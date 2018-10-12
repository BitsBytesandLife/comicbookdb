using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace ComicDatabaseProject
{
    class DapperComicBookDBQueries
    {
        private static string connectionString;

        public DapperComicBookDBQueries(string _connectionString)
        {
            connectionString = _connectionString;
        }

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


        public decimal GetTotalValue()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<decimal>("SELECT sum(currentValue) as totalValue " +
                                                    "FROM comicvalue;").Single();
            }
        }
    }
}
