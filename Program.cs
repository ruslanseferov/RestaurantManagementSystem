using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Repositories;
using RestaurantManagementSystem.Services;
using RestaurantManagementSystem.UI;
using System;

namespace RestaurantManagementSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new RestaurantContext();
            await context.Database.MigrateAsync();

            // Repositories
            var restaurantRepo = new RestaurantRepository(context);
            var tableRepo      = new TableRepository(context);
            var menuItemRepo   = new MenuItemRepository(context);
            var orderRepo      = new OrderRepository(context);

            // Services
            var restaurantService = new RestaurantService(restaurantRepo);
            var tableService      = new TableService(tableRepo, restaurantRepo);
            var menuItemService   = new MenuItemService(menuItemRepo);
            var orderService      = new OrderService(orderRepo, restaurantRepo, tableRepo, menuItemRepo);
            var statsService      = new StatisticsService(restaurantRepo, context);

            // Seed on first run
            await SeedData.InitializeAsync(restaurantService, tableService, menuItemService, orderService, context);

            // Main loop
            while (true)
            {
                Console.WriteLine("\n=== ANA MENYU ===");
                Console.WriteLine("1. Restoranlar");
                Console.WriteLine("2. Masalar");
                Console.WriteLine("3. Menyu (Yemekler)");
                Console.WriteLine("4. Sifarisler");
                Console.WriteLine("5. Statistika");
                Console.WriteLine("0. Cixis");
                Console.Write("Secim: ");

                switch (Console.ReadLine())
                {
                    case "1": await RestaurantMenu.ShowAsync(restaurantService); break;
                    case "2": await TableMenu.ShowAsync(tableService);           break;
                    case "3": await MenuItemMenu.ShowAsync(menuItemService);     break;
                    case "4": await OrderMenu.ShowAsync(orderService);           break;
                    case "5": await StatsMenu.ShowAsync(statsService);           break;
                    case "0": return;
                    default:  Console.WriteLine("Yanlis secim.");                break;
                }
            }
        }
    }
}
