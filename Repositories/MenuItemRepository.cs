using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly RestaurantContext _context;
        public MenuItemRepository(RestaurantContext context) => _context = context;

        public async Task<MenuItem?> GetByIdAsync(int id) =>
            await _context.MenuItems.FindAsync(id);

        public async Task<List<MenuItem>> GetByRestaurantAsync(int restaurantId) =>
            await _context.MenuItems
                .Where(m => m.RestaurantId == restaurantId)
                .OrderBy(m => m.Name)
                .ToListAsync();

        public async Task AddAsync(MenuItem item)
        {
            await _context.MenuItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuItem item)
        {
            _context.MenuItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item != null)
            {
                _context.MenuItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int restaurantId, string name) =>
            await _context.MenuItems
                .AnyAsync(m => m.RestaurantId == restaurantId && m.Name == name);
    }
}
