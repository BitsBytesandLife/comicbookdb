using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;


namespace ComicDatabaseProject
{

    
    class Program
    {
        private static string connectionString;
        private static comicBookRepository cbRepo;
        private static comicDetailsRepository cbdRepo;
        private static comicValueRepositiory cbvRepo;
        private static DapperComicBookDBQueries cbqRepo;
        private static cbAppUI cbUI;
         
        public static void Main(string[] args)
        {

            //setting Foreground
            Console.ForegroundColor = ConsoleColor.Gray;
            //Setting Application flies
            ConsoleAppSettings();
            
            //SetRepos
            cbRepo = new comicBookRepository(connectionString);
            cbdRepo = new comicDetailsRepository(connectionString);
            cbvRepo = new comicValueRepositiory(connectionString);
            cbqRepo = new DapperComicBookDBQueries(connectionString);
            cbUI = new cbAppUI();


            

            RunMenu();
            
            Console.ReadLine();
        }


        /// <summary>
        /// This method hides the connection from GitHub
        /// </summary>
        public static void ConsoleAppSettings()
        {

            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsetting.json")
#if DEBUG
                .AddJsonFile("appsetting.Debug.json")
#endif
                .Build();


            connectionString = config.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// This method runs the menu. 
        /// It clears the screen, displays the menu, and runs the menu options.
        /// </summary>
        public static void RunMenu()
        {
            Console.Clear();
            DisplayMenu();
            RunMenuOptions();
        }

        /// <summary>
        /// This method prompts the end-user is they want 
        /// to continue or exit the application. 
        /// </summary>
        public static void ContinueApp()
        {

            bool userResponse =   cbAppUI.GetUserConsent("Would you like to continue?");
            
            if (userResponse == true)
            {
                Console.Clear();
                DisplayMenu();
                RunMenuOptions();
            }
            else
            Console.WriteLine("Exit the application");
            return; 
        }

        /// <summary>
        /// This method displays the header for the menu from string.
        /// </summary>
        public static void MenuHeader(string header)
        {
            Console.Clear();
            Console.WriteLine($"---- {header} ---");
        }

        /// <summary>
        /// This method displays the menu.
        /// </summary>
        public static void DisplayMenu()
        {
            
            Console.WriteLine("Comic Book Database");
            Console.WriteLine("1   Add a new comic book");
            Console.WriteLine("2   Add a detail about comic book");
            Console.WriteLine("3   Add value for a comic book");
            Console.WriteLine("4   Update an comic book");
            Console.WriteLine("5   Update the condition of a comic");
            Console.WriteLine("6   Update detail about a comic");
            Console.WriteLine("7   Update current price of comic book");
            Console.WriteLine("8   Delete a comic book");
            Console.WriteLine("9   Delete a detail about a comic");
            Console.WriteLine("10  Delete value comic book");
            Console.WriteLine("11  Show Comic Collection");
            Console.WriteLine("12  Show Details About Comics");
            Console.WriteLine("13  Show Financial about a Collection");
            Console.WriteLine("14  Show Total Value the Collection");
            Console.WriteLine("15  Report Show All Comics");
            Console.WriteLine("16  Search by Title");
            Console.WriteLine("17  Exit");
        }

        /// <summary>
        /// This method takes the menu choice from the end-user
        /// Then runs the option.
        /// </summary>
        public static void RunMenuOptions()
        {
            string menuItem = cbAppUI.ValidateChoices("Select a type in a number from 1 - 14 \n" +
                                                      "to choose your menu item", new string[] { "1", "2", "3", "4", "5", "6",
                                                      "7", "8", "9", "10", "11", "12","13","14","15","16","17"});
                

            switch (menuItem)
            {
                case "1":
                    MenuHeader("Add a new comic book to the collection");
                    InsertCB();         
                    ContinueApp();
                    break;
                case "2":
                    MenuHeader("Add a detail about comic book");
                    InsertDetail();
                    ContinueApp();
                    break;
                case "3":
                    MenuHeader("Add a Value about comic book");
                    InsertCBVaule();
                    ContinueApp();
                    break;
                case "4":
                    MenuHeader("Update an comic book");
                    UpdateCB();
                    MenuHeader("Comic Book ");
                    ContinueApp();
                    break;
                case "5":
                    MenuHeader("Update the condition of a comic book");
                    UpdateCBCondition();
                    ContinueApp();
                    break;
                case "6":
                    MenuHeader("Update an Detail");
                    UpdateDetail();
                    ContinueApp();
                    break;
                case "7":
                    MenuHeader("Update the Current Value of a Comic:");
                    UpdateCurrentValue();
                    ContinueApp();
                    break;
                case "8":
                    Console.ForegroundColor = ConsoleColor.Red;
                    MenuHeader("Delete a comic book");
                    DeleteCB();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ContinueApp();
                    break;
                case "9":
                    Console.ForegroundColor = ConsoleColor.Red;
                    MenuHeader("Delete an Detail");
                    DeleteDetail();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ContinueApp();
                    break;
                case "10":
                    MenuHeader("Delete an Financial Record ");
                    DeleteValue();
                    ContinueApp();
                    break;
                case "11":
                    MenuHeader("Show Comic Collection");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    cbRepo.GetComicbooks();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ContinueApp();
                    break;
                case "12":
                    MenuHeader("Show Details About Comics");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    cbdRepo.ShowDetails();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ContinueApp();
                    break;
                case "13":
                    MenuHeader("Show financial about your comics");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    cbvRepo.GetComicValues();
                    ContinueApp();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "14":
                    MenuHeader("Show Comic Value");
                    Console.ForegroundColor = ConsoleColor.Green;
                    var qResults = cbqRepo.GetTotalValue();
                    Console.WriteLine($"Total Value of the Collection: {qResults}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ContinueApp(); 
                    break;
                case "15":
                    MenuHeader("Report Show All Comics");
                    showResults(cbqRepo.GetComicInfo(), ConsoleColor.Yellow, ConsoleColor.Gray);
                    return;
                case "16":
                    MenuHeader("Search by Title");
                    
                    var qSearchTitle = cbAppUI.ValidateString("Enter the Title for the comic book:");
                    showResults(cbqRepo.GetComicInfo(qSearchTitle), ConsoleColor.Yellow, ConsoleColor.Gray);
                    ContinueApp();
                    return;
                case "17":
                    MenuHeader("Exit press any anything to exit the Comic Database");
                    break;

            }

        }


        public static void showResults(List<ComicBookQueries> searchResults, ConsoleColor colorChange, ConsoleColor colorReset)        {
            Console.ForegroundColor = colorChange;
            foreach (var searchResult in searchResults)
            {
                Console.WriteLine($"Title: {searchResult.title}  Issue:{searchResult.issue} Publisher: {searchResult.publisher} \n" +
                                  $"Condition: {searchResult.cbcondtition}  Detail: {searchResult.detail} Current Value:{searchResult.currentValue} \n\n");
            }
            Console.ForegroundColor = colorReset;
        }

        public static void InsertCB()
        {
            comicbooks cbInsert = new comicbooks();

            //cbInsert.title = cbAppUI.ValidateString("Enter the Title for the comic book:", Console.ReadLine());
           
            cbInsert.title = cbAppUI.ValidateString("Enter the Title for the comic book:"); 
            cbInsert.issue = cbAppUI.ValidateInteger("Enter the Issue Number for the comic book:");
            cbInsert.publisher = cbAppUI.ValidateChoices("Enter the Publisher for the comic book (DC,MARVEL,\n" +
                                                         "DARK HORSE, TOP COW, MAD MAGAZINE,IMAGE):", 
                                                         new string[] { "DC", "MARVEL", "DARK HORSE", "TOP COW", "MAD MAGAZINE", "IMAGE" });
            cbInsert.comicBookCondition = cbAppUI.ValidateChoices("Enter the Condition for the comic book (MINT, NEAR MINT, \n" +
                                                                  "GOOD,NEAR GOOD,POOR):",
                                                                  new string[] { "MINT", "NEAR MINT", "GOOD", "NEAR GOOD", "POOR" });
            cbRepo.CreateCBRecord(cbInsert);
        }

        public static void UpdateCB()
        {
            comicbooks cbUpdate = new comicbooks();

            
            cbUpdate.title = cbAppUI.ValidateString("Enter the Title for the comic book:");

            cbUpdate.issue = cbAppUI.ValidateInteger("Enter the Issue Number for the comic book:");
            

            cbUpdate.publisher = cbAppUI.ValidateChoices("Enter the Publisher for the comic book (DC,Marvel,\n" +
                                                         "Dark Horse, Top Cow, Mad Magazine,Image):",
                                                         new string[] { "DC", "MARVEL", "DARK HORSE", "TOP COW", "MAD MAGAZINE", "IMAGE" });

            cbUpdate.comicBookCondition = cbAppUI.ValidateChoices("Enter the Condition for the comic book (MINT, NEAR MINT, \n" +
                                                                  "GOOD,NEAR GOOD,POOR):",
                                                                  new string[] { "MINT", "NEAR MINT", "GOOD", "NEAR GOOD", "POOR" });
            cbUpdate.comicBookID = cbAppUI.ValidateInteger("Enter the Comic Book ID that you want to Update");


            cbRepo.UpdateComicBookAll(cbUpdate);
        }


        public static void UpdateCBCondition()
        {
            comicbooks cbUpdateCondition = new comicbooks();
            cbUpdateCondition.comicBookID = cbAppUI.ValidateInteger("Enter the ComicBookID that you wish to update:");
            cbUpdateCondition.comicBookCondition = cbAppUI.ValidateChoices("Enter the Condition for the comic book (MINT, NEAR MINT, \n" +
                                                                  "GOOD,NEAR GOOD,POOR):",
                                                                  new string[] { "MINT", "NEAR MINT", "GOOD", "NEAR GOOD", "POOR" });
            cbRepo.UpdateCBCondition(cbUpdateCondition);
        }

        public static void DeleteCB()
        {
            var cbID = cbAppUI.ValidateInteger("Enter the Comic Book Id that you wish to delete:");
            cbRepo.DeleteComicBookRecord(cbID);
        }


        public static void InsertDetail()
        {
            comicDetails cbd = new comicDetails();

            //Console.WriteLine("Enter the ComicBooKID that you want insert:");
            cbd.comicBookID = cbAppUI.ValidateInteger("Enter the ComicBooKID that you want insert:");
            Console.WriteLine("Enter the Detail for the comic book:");
            cbd.detail = cbAppUI.ValidateString("Enter the Detail for the comic book:");
            

            cbdRepo.CreateComicDetailRecord(cbd);

        }

        public static void UpdateDetail()
        {

            comicDetails cbd = new comicDetails();

            Console.WriteLine("Enter the ComicBooKID that you want update:");
            cbd.comicBookID = cbAppUI.ValidateInteger("Enter the ComicBooKID that you want update:");
            Console.WriteLine("Enter the Detail for the comic book:");
            cbd.detail = Console.ReadLine();
            while (string.IsNullOrEmpty(cbd.detail))
            {
                Console.WriteLine("Detail field can't be empty! Enter the Details again.");
                cbd.detail = Console.ReadLine();
            }

            cbdRepo.UpdateComicDetailRecord(cbd);

        }

        public static void DeleteDetail()
        {
            comicDetails cbd = new comicDetails();
            var cbDID = cbAppUI.ValidateInteger("Enter the ComicValueID: ");
            cbdRepo.DeleteComicDetailRecord(cbDID);

        }

        public static void InsertCBVaule()
        {
            comicValue cv = new comicValue();
            
            cv.comicId = cbAppUI.ValidateInteger("Enter the ComicValueID: ");
            cv.originalPrice = cbAppUI.ValidateDecimal("Enter the original value of the comic book:");
            cv.currentValue = cbAppUI.ValidateDecimal("Enter the current value of the comic book:");

            cbvRepo.CreateComicValueRecord(cv);
        }

        public static void UpdateCurrentValue()
        {
            comicValue cv = new comicValue();
            cv.comicValueID = cbAppUI.ValidateInteger("Enter the ComicValueID: ");
            cv.currentValue = cbAppUI.ValidateDecimal("Enter the current value of the comic book:");
            cbvRepo.UpdateComicValueRecord(cv);
        }

        public static void DeleteValue()
        {
            comicValue cv = new comicValue();
            Console.WriteLine("Enter the comicValueID that you want Delete:");
            cv.comicValueID = cbAppUI.ValidateInteger("Enter the ID that you want Delete:");
            
            cbvRepo.DeleteComicValuesRecord(cv);
        }

        

    }
}




