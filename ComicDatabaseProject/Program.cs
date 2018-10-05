using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ComicDatabaseProject
{

    
    class Program
    {
        private static string connectionString;

        public static void Main(string[] args)
        {
            //Setting Application flies
            ConsoleAppSettings();




            Console.ReadLine();
        }



        public static void ConsoleAppSettings()
        {

            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsetting.json")
#if DEBUG
                .AddJsonFile("appsetting.Debug.json")
#endif
                .Build();


            string connString = config.GetConnectionString("DefaultConnection");

            comicBookRepository cbRepo = new comicBookRepository(connectionString);


        }
    }

   


}

