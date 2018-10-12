using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComicDatabaseProject
{
    class cbAppUI
    {


        public static bool GetUserConsent(string question)

        {
            string[] yesOptions = { "Y", "YES", "YEP", "SURE", "OK", "YEAH" };
            string[] noOptions = { "N", "NO", "NOPE", "NAH" };

            while (true)
            {
                Console.WriteLine(question);
                string userResponse = Console.ReadLine().Trim();

                if (yesOptions.Contains(userResponse.ToUpper()))
                {
                    return true;
                }
                else if (noOptions.Contains(userResponse.ToUpper()))
                {
                    return false;
                }
            }

        }

        public static string SetPublisher(string question)
        {
            string[] validPublisher = { "DC", "Marvel", "Top Cow", "Mad Magazine", "Image" };

            while (true)

            {
                Console.WriteLine(question);
                string userResponse = Console.ReadLine().Trim();

                if (validPublisher.Contains(userResponse.ToLower()))
                {
                    return userResponse;
                }

            }

        }

        public static string SetCondition(string question)
        {
            string[] validCondition = { "Mint", "Near Mint", "Good", "Near Good", "Poor" };
            while (true)
            {
                Console.WriteLine(question);
                string userResponse = Console.ReadLine().Trim();

                if (validCondition.Contains(userResponse))
                {
                    return userResponse;
                }
            }
        }

        public static string ValidationMenuChoice(string question)

        {

            string[] menuOptions = { "1", "2", "3", "4", "5", "6",
                                 "7", "8", "9", "10", "11", "12",
                                 "13","14"};

            while (true)
            {
                Console.WriteLine(question);
                string userResponse = Console.ReadLine().Trim();

                if (menuOptions.Contains(userResponse))
                {
                    return userResponse;
                }

            }
        }

        public static int ValidateInteger(string question)
        {
            int number = 0;
            string str = "";
            while (!int.TryParse(str, out number))
            {
                Console.WriteLine(question);
                str = Console.ReadLine();
            }
            return number;
        }


        

        public static decimal ValidateDecimal(string question)
        {
            string str = "";
            decimal dec = 0;
            while (!Decimal.TryParse(str, out dec))
            {
                Console.WriteLine(question);
                str = Console.ReadLine();
            }
            return dec;
        }

        public static string ValidateString(string question)
        {
            string str = "";
            while (string.IsNullOrEmpty(str))
            {
                Console.WriteLine(question);
                str = Console.ReadLine();
            }

            return str;
        }

        public static string ValidateChoices(string question, string [] validCondition)
        {
            
            while (true)
            {
                Console.WriteLine(question);
                string userResponse = Console.ReadLine().Trim().ToUpper();

                if (validCondition.Contains(userResponse))
                {
                    return userResponse;
                }
            }
        }

    }
}