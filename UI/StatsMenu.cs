using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.UI
{
    public static class StatsMenu
    {
        public static async Task ShowAsync(StatisticsService service)
        {
            Console.WriteLine("\n1. Restoranin veziyyeti  2. Siralama  3. En cox satilan (restoran)  4. En cox satilan (umumi)");
            Console.Write("Secim: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int id1);
                    await service.ShowRestaurantStatusAsync(id1);
                    break;
                case "2":
                    await service.ShowRestaurantRankingsAsync();
                    break;
                case "3":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int id3);
                    await service.ShowTopMenuItemsAsync(id3);
                    break;
                case "4":
                    await service.ShowGlobalTopMenuItemsAsync();
                    break;
                default:
                    Console.WriteLine("Yanlis secim.");
                    break;
            }
        }
    }
}
