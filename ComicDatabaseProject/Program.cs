using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ComicDatabaseProject
{

    
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()

                .SetBasePath(Directory.GetCurrentDirectory())

                .AddJsonFile("appsettings.json")

#if DEBUG

                .AddJsonFile("appsettings.Debug.json")

#else

                .AddJsonFile("appsettings.Release.json")

#endif

                .Build();

            connString = config.GetConnectionString("DefaultConnection");

            Console.ReadLine();
        }


    }

    
}
