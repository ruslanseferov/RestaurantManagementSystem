using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagementSystem.Data;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext _context;
        public TableRepository(RestaurantContext context) => _context = context;

        public async Task<Table?> GetByIdAsync(int id) =>
            await _context.Tables.FindAsync(id);

        public async Task<List<Table>> GetByRestaurantAsync(int restaurantId) =>
            await _context.Tables
                .Where(t => t.RestaurantId == restaurantId)
                .OrderBy(t => t.TableNumber)
                .ToListAsync();

        public async Task AddAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int restaurantId, int tableNumber) =>
            await _context.Tables
                .AnyAsync(t => t.RestaurantId == restaurantId && t.TableNumber == tableNumber);
    }
}
