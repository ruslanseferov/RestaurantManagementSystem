using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Services
{
    public class MenuItemService
    {
        private readonly IMenuItemRepository _repo;
        public MenuItemService(IMenuItemRepository repo) => _repo = repo;

        public async Task AddMenuItemAsync(int restaurantId, string name, decimal price, string category)
        {
            if (await _repo.ExistsAsync(restaurantId, name))
            {
                Console.WriteLine("Bu restoranda eyni adda yemek movcuddur."); return;
            }
            await _repo.AddAsync(new MenuItem
            {
                RestaurantId = restaurantId,
                Name = name,
                Price = price,
                Category = category,
                TotalSold = 0
            });
            Console.WriteLine("Yemek elave edildi.");
        }

        public async Task UpdateMenuItemAsync(int id, string name, decimal price, string category)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) { Console.WriteLine("Yemek tapilmadi."); return; }
            item.Name = name;
            item.Price = price;
            item.Category = category;
            await _repo.UpdateAsync(item);
            Console.WriteLine("Yemek yenilendi.");
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) { Console.WriteLine("Yemek tapilmadi."); return; }
            await _repo.DeleteAsync(id);
            Console.WriteLine("Yemek silindi.");
        }

        public async Task ListMenuItemsAsync(int restaurantId)
        {
            var items = await _repo.GetByRestaurantAsync(restaurantId);
            Console.WriteLine();
            Console.WriteLine($"{"ID",-5} {"Ad",-25} {"Qiymet",10} {"Kateqoriya",-15} {"Satis",8}");
            Console.WriteLine(new string('-', 68));
            foreach (var i in items)
                Console.WriteLine($"{i.Id,-5} {i.Name,-25} {i.Price,10:F2} {i.Category,-15} {i.TotalSold,8}");
            Console.WriteLine();
        }
    }
}
