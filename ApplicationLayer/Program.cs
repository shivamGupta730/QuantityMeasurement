using System;

using ModelLayer.Entity;
using ModelLayer;
using BusinessLayer.Services;
using ApplicationLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Repository;

namespace ApplicationLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🌟 Welcome to Quantity Measurement System! 🌟");
            Console.WriteLine("===========================================");
            
            Console.WriteLine("\nPlease select storage type for measurements:");
            Console.WriteLine("1. Cache Repository (In-memory, fast but temporary)");
            Console.WriteLine("2. Database Repository (Persistent storage)");
            Console.Write("Enter choice (1 or 2): ");

            bool useDatabaseRepository;
            while (true)
            {
                string choice = Console.ReadLine()?.Trim();
                if (choice == "1")
                {
                    useDatabaseRepository = false;
                    Console.WriteLine("✅ Using Cache Repository");
                    break;
                }
                else if (choice == "2")
                {
                    useDatabaseRepository = true;
                    Console.WriteLine("✅ Using Database Repository");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 1 or 2: ");
                }
            }

            IQuantityMeasurementRepository repository = useDatabaseRepository
                ? new QuantityMeasurementDatabaseRepository()
                : new QuantityMeasurementCacheRepository();

            var service = new QuantityMeasurementService(repository);

            IMainMenu menu = new MainMenu(service);
            menu.ShowMainMenu();
        }
      
    }
}
