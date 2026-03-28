using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Services
{
    public class StatisticsService
    {
        private readonly IRestaurantRepository _repo;
        private readonly RestaurantContext _context;

        public StatisticsService(IRestaurantRepository repo, RestaurantContext context)
        {
            _repo = repo;
            _context = context;
        }

        // Report 1: Single restaurant current status
        public async Task ShowRestaurantStatusAsync(int restaurantId)
        {
            var restaurant = await _repo.GetByIdAsync(restaurantId);
            if (restaurant == null) { Console.WriteLine("Restoran tapilmadi."); return; }

            Console.WriteLine();
            Console.WriteLine($"{"Restoran",-25} {"Sifaris",10} {"Satis (AZN)",15} {"Aktiv Masa",12}");
            Console.WriteLine(new string('-', 65));
            Console.WriteLine(
                $"{restaurant.Name,-25} {restaurant.TotalOrders,10} {restaurant.TotalRevenue,15:F2} {restaurant.ActiveTables,12}");
            Console.WriteLine();
        }

        // Report 2: All restaurants ranked by total revenue (descending)
        public async Task ShowRestaurantRankingsAsync()
        {
            var restaurants = await _repo.GetAllAsync();

            Console.WriteLine();
            Console.WriteLine($"{"Restoran",-25} {"Sifaris",10} {"Satis (AZN)",15}");
            Console.WriteLine(new string('-', 53));
            foreach (var r in restaurants)
                Console.WriteLine($"{r.Name,-25} {r.TotalOrders,10} {r.TotalRevenue,15:F2}");
            Console.WriteLine();
        }

        // Report 3: Most sold menu items for a specific restaurant
        public async Task ShowTopMenuItemsAsync(int restaurantId)
        {
            var items = await _repo.GetMenuItemsByRestaurantAsync(restaurantId);

            Console.WriteLine();
            Console.WriteLine($"{"Yemek",-25} {"Satis sayi",12}");
            Console.WriteLine(new string('-', 38));
            foreach (var item in items)
                Console.WriteLine($"{item.Name,-25} {item.TotalSold,12}");
            Console.WriteLine();
        }

        // Report 4: Global top menu items across all restaurants
        public async Task ShowGlobalTopMenuItemsAsync()
        {
            var items = await _context.MenuItems
                .OrderByDescending(m => m.TotalSold)
                .ToListAsync();

            Console.WriteLine();
            Console.WriteLine($"{"Yemek",-25} {"Restoran ID",12} {"Satis sayi",12}");
            Console.WriteLine(new string('-', 52));
            foreach (var item in items)
                Console.WriteLine($"{item.Name,-25} {item.RestaurantId,12} {item.TotalSold,12}");
            Console.WriteLine();
        }
    }
}
