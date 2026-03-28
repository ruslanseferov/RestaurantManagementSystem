using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repositories.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetByIdAsync(int id);
        Task<List<Restaurant>> GetAllAsync();
        Task AddAsync(Restaurant restaurant);
        Task UpdateAsync(Restaurant restaurant);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(string name, int branchCode);
        Task<List<MenuItem>> GetMenuItemsByRestaurantAsync(int restaurantId);
    }
}
