using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Services
{
    public class RestaurantService
    {
        private readonly IRestaurantRepository _repo;
        public RestaurantService(IRestaurantRepository repo) => _repo = repo;

        public async Task AddRestaurantAsync(string name, int branchCode)
        {
            if (branchCode < 1 || branchCode > 99)
            {
                Console.WriteLine("Filial kodu 1-99 arasinda olmalidir."); return;
            }
            if (await _repo.ExistsAsync(name, branchCode))
            {
                Console.WriteLine("Bu ad ve ya filial kodu artiq movcuddur."); return;
            }
            await _repo.AddAsync(new Restaurant
            {
                Name = name,
                BranchCode = branchCode,
                TotalOrders = 0,
                TotalRevenue = 0,
                ActiveTables = 0
            });
            Console.WriteLine("Restoran elave edildi.");
        }

        public async Task UpdateRestaurantAsync(int id, string name, int branchCode)
        {
            var restaurant = await _repo.GetByIdAsync(id);
            if (restaurant == null) { Console.WriteLine("Restoran tapilmadi."); return; }
            if (branchCode < 1 || branchCode > 99)
            {
                Console.WriteLine("Filial kodu 1-99 arasinda olmalidir."); return;
            }
            restaurant.Name = name;
            restaurant.BranchCode = branchCode;
            await _repo.UpdateAsync(restaurant);
            Console.WriteLine("Restoran yenilendi.");
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            var restaurant = await _repo.GetByIdAsync(id);
            if (restaurant == null) { Console.WriteLine("Restoran tapilmadi."); return; }
            await _repo.DeleteAsync(id);
            Console.WriteLine("Restoran silindi.");
        }

        public async Task ListRestaurantsAsync()
        {
            var list = await _repo.GetAllAsync();
            Console.WriteLine();
            Console.WriteLine($"{"ID",-5} {"Ad",-25} {"Filial Kodu",12}");
            Console.WriteLine(new string('-', 45));
            foreach (var r in list)
                Console.WriteLine($"{r.Id,-5} {r.Name,-25} {r.BranchCode,12}");
            Console.WriteLine();
        }
    }
}
