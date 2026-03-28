using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            RestaurantService restaurantService,
            TableService tableService,
            MenuItemService menuItemService,
            OrderService orderService,
            RestaurantContext context)
        {
            // Skip if already seeded
            if (await context.Restaurants.AnyAsync()) return;

            Console.WriteLine("Melumatlar yuklenir...");

            // Add restaurants
            string[] restaurantNames = { "Elite Restoran", "City Grill", "Downtown Cafe" };
            int[] branchCodes = { 1, 2, 3 };
            for (int i = 0; i < restaurantNames.Length; i++)
                await restaurantService.AddRestaurantAsync(restaurantNames[i], branchCodes[i]);

            // Retrieve DB-assigned IDs
            var restaurants = await context.Restaurants.OrderBy(r => r.Id).ToListAsync();
            int[] restaurantIds = restaurants.Select(r => r.Id).ToArray();

            // Add 5 tables per restaurant
            foreach (int rid in restaurantIds)
                for (int t = 1; t <= 5; t++)
                    await tableService.AddTableAsync(rid, t, 4);

            // Add menu items per restaurant
            foreach (int rid in restaurantIds)
            {
                await menuItemService.AddMenuItemAsync(rid, "Pizza",  12.00m, "Ana Yemek");
                await menuItemService.AddMenuItemAsync(rid, "Burger",  8.00m, "Fast Food");
                await menuItemService.AddMenuItemAsync(rid, "Pasta",  10.00m, "Ana Yemek");
                await menuItemService.AddMenuItemAsync(rid, "Steak",  25.00m, "Ana Yemek");
            }

            // Retrieve table and menu item IDs
            var allTables    = await context.Tables.OrderBy(t => t.Id).ToListAsync();
            var allMenuItems = await context.MenuItems.OrderBy(m => m.Id).ToListAsync();

            // Seed per-item sale counts to match the spec's statistics tables:
            // Elite Restoran  → Pizza=145, Burger=120, Pasta=98,  Steak=85
            // City Grill      → Pizza=110, Burger=95,  Pasta=75,  Steak=60
            // Downtown Cafe   → Pizza=90,  Burger=75,  Pasta=65,  Steak=50
            var seedPlan = new[]
            {
                (rIdx: 0, pizza: 145, burger: 120, pasta: 98,  steak: 85),
                (rIdx: 1, pizza: 110, burger:  95, pasta: 75,  steak: 60),
                (rIdx: 2, pizza:  90, burger:  75, pasta: 65,  steak: 50),
            };

            foreach (var (rIdx, pizza, burger, pasta, steak) in seedPlan)
            {
                int rid     = restaurantIds[rIdx];
                int tableId = allTables.First(t => t.RestaurantId == rid).Id;
                var itemIds = allMenuItems.Where(m => m.RestaurantId == rid).ToList();

                int pizzaId  = itemIds.First(m => m.Name == "Pizza").Id;
                int burgerId = itemIds.First(m => m.Name == "Burger").Id;
                int pastaId  = itemIds.First(m => m.Name == "Pasta").Id;
                int steakId  = itemIds.First(m => m.Name == "Steak").Id;

                for (int i = 0; i < pizza;  i++) await orderService.AddOrderAsync(rid, tableId, new Dictionary<int, int> { { pizzaId,  1 } });
                for (int i = 0; i < burger; i++) await orderService.AddOrderAsync(rid, tableId, new Dictionary<int, int> { { burgerId, 1 } });
                for (int i = 0; i < pasta;  i++) await orderService.AddOrderAsync(rid, tableId, new Dictionary<int, int> { { pastaId,  1 } });
                for (int i = 0; i < steak;  i++) await orderService.AddOrderAsync(rid, tableId, new Dictionary<int, int> { { steakId,  1 } });
            }

            Console.WriteLine("Melumatlar ugurla yuklendi!\n");
        }
    }
}
