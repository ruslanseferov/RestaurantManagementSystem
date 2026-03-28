using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repositories.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<MenuItem?> GetByIdAsync(int id);
        Task<List<MenuItem>> GetByRestaurantAsync(int restaurantId);
        Task AddAsync(MenuItem item);
        Task UpdateAsync(MenuItem item);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int restaurantId, string name);
    }
}
