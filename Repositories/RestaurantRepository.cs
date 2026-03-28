using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantContext _context;
        public RestaurantRepository(RestaurantContext context) => _context = context;

        public async Task<Restaurant?> GetByIdAsync(int id) =>
            await _context.Restaurants
                .Include(r => r.Tables)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<List<Restaurant>> GetAllAsync() =>
            await _context.Restaurants
                .OrderByDescending(r => r.TotalRevenue)
                .ToListAsync();

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string name, int branchCode) =>
            await _context.Restaurants
                .AnyAsync(r => r.Name == name || r.BranchCode == branchCode);

        public async Task<List<MenuItem>> GetMenuItemsByRestaurantAsync(int restaurantId) =>
            await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .OrderByDescending(m => m.TotalSold)
                .ToListAsync();
    }
}
