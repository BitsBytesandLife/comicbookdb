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

        public List<comicbooks> GetComicInfo()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<comicbooks>("SELECT title, issue, p.publisherName as publisher, c.comicBookCondition as `Condition`, cd.detail,cv.currentValue " +
                                              "FROM comicbooks cb " +
                                              "INNER JOIN publisher p " +
                                              "ON p.publisherID = cb.Publisher " +
                                              "INNER JOIN `condition` c " +
                                              "ON c.conditionID = cb.comicBookCondition " +
                                              "INNER JOIN comicdetails cd " +
                                              "ON cd.ComicBookDetailID = cb.comicdetail " +
                                              "INNER JOIN comicvalue cv " +
                                              "ON cv.comicBookID = cb.comicBookID; ").ToList();
            }
        }

        public List<comicbooks> GetComicValueInfo()
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                return conn.Query<comicbooks>("SELECT cb.title, cb.issue,cv.orginalPrice,cv.currentValue " + 
                                              "FROM comicbookdb.comicvalue cv " +
                                              "INNER JOIN comicbookdb.comicbooks cb " +
                                              "ON cv.comicBookID = cb.comicBookID;").ToList();
            }
        }
    }
}
