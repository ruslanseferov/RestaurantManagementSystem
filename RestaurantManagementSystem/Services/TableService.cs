using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Services
{
    public class TableService
    {
        private readonly ITableRepository _tableRepo;
        private readonly IRestaurantRepository _restaurantRepo;

        public TableService(ITableRepository tableRepo, IRestaurantRepository restaurantRepo)
        {
            _tableRepo = tableRepo;
            _restaurantRepo = restaurantRepo;
        }

        public async Task AddTableAsync(int restaurantId, int tableNumber, int capacity)
        {
            var restaurant = await _restaurantRepo.GetByIdAsync(restaurantId);
            if (restaurant == null) { Console.WriteLine("Restoran tapilmadi."); return; }

            if (await _tableRepo.ExistsAsync(restaurantId, tableNumber))
            {
                Console.WriteLine("Bu restoranda eyni masa nomresi movcuddur."); return;
            }

            await _tableRepo.AddAsync(new Table
            {
                RestaurantId = restaurantId,
                TableNumber = tableNumber,
                Capacity = capacity,
                OrderCount = 0
            });

            restaurant.ActiveTables++;
            await _restaurantRepo.UpdateAsync(restaurant);

            Console.WriteLine("Masa elave edildi.");
        }

        public async Task UpdateTableAsync(int id, int tableNumber, int capacity)
        {
            var table = await _tableRepo.GetByIdAsync(id);
            if (table == null) { Console.WriteLine("Masa tapilmadi."); return; }

            if (table.TableNumber != tableNumber &&
                await _tableRepo.ExistsAsync(table.RestaurantId, tableNumber))
            {
                Console.WriteLine("Bu restoranda eyni masa nomresi movcuddur."); return;
            }

            table.TableNumber = tableNumber;
            table.Capacity = capacity;
            await _tableRepo.UpdateAsync(table);
            Console.WriteLine("Masa yenilendi.");
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _tableRepo.GetByIdAsync(id);
            if (table == null) { Console.WriteLine("Masa tapilmadi."); return; }

            var restaurant = await _restaurantRepo.GetByIdAsync(table.RestaurantId);
            await _tableRepo.DeleteAsync(id);

            if (restaurant != null)
            {
                restaurant.ActiveTables = Math.Max(0, restaurant.ActiveTables - 1);
                await _restaurantRepo.UpdateAsync(restaurant);
            }

            Console.WriteLine("Masa silindi.");
        }

        public async Task ListTablesAsync(int restaurantId)
        {
            var tables = await _tableRepo.GetByRestaurantAsync(restaurantId);
            Console.WriteLine();
            Console.WriteLine($"{"ID",-5} {"Masa No",8} {"Tutum",8} {"Sifaris",10}");
            Console.WriteLine(new string('-', 35));
            foreach (var t in tables)
                Console.WriteLine($"{t.Id,-5} {t.TableNumber,8} {t.Capacity,8} {t.OrderCount,10}");
            Console.WriteLine();
        }
    }
}
