using System.Collections.Generic;

namespace RestaurantManagementSystem.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public int RestaurantId { get; set; }
        public int OrderCount { get; set; }

        public Restaurant Restaurant { get; set; } = null!;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
