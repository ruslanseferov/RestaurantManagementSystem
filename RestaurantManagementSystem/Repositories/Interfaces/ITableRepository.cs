using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repositories.Interfaces
{
    public interface ITableRepository
    {
        Task<Table?> GetByIdAsync(int id);
        Task<List<Table>> GetByRestaurantAsync(int restaurantId);
        Task AddAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int restaurantId, int tableNumber);
    }
}
