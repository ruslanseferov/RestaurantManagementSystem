using System.Collections.Generic;

namespace RestaurantManagementSystem.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int BranchCode { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ActiveTables { get; set; }

        public List<Table> Tables { get; set; } = new List<Table>();
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
