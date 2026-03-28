using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetByRestaurantAsync(int restaurantId);
        Task AddAsync(Order order);
        Task DeleteAsync(int id);
    }
}
