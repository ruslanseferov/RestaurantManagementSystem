using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Repositories.Interfaces;

namespace RestaurantManagementSystem.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IRestaurantRepository _restaurantRepo;
        private readonly ITableRepository _tableRepo;
        private readonly IMenuItemRepository _menuItemRepo;

        public OrderService(
            IOrderRepository orderRepo,
            IRestaurantRepository restaurantRepo,
            ITableRepository tableRepo,
            IMenuItemRepository menuItemRepo)
        {
            _orderRepo = orderRepo;
            _restaurantRepo = restaurantRepo;
            _tableRepo = tableRepo;
            _menuItemRepo = menuItemRepo;
        }

        public async Task AddOrderAsync(int restaurantId, int tableId, Dictionary<int, int> menuItemQuantities)
        {
            var restaurant = await _restaurantRepo.GetByIdAsync(restaurantId);
            if (restaurant == null) { Console.WriteLine("Restoran tapilmadi."); return; }

            var table = await _tableRepo.GetByIdAsync(tableId);
            if (table == null || table.RestaurantId != restaurantId)
            {
                Console.WriteLine("Bu restorana aid masa tapilmadi."); return;
            }

            if (menuItemQuantities.Count == 0)
            {
                Console.WriteLine("Sifaris bos ola bilmez."); return;
            }

            var order = new Order
            {
                RestaurantId = restaurantId,
                TableId = tableId,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };

            decimal total = 0;
            foreach (var (menuItemId, quantity) in menuItemQuantities)
            {
                var menuItem = await _menuItemRepo.GetByIdAsync(menuItemId);
                if (menuItem == null || menuItem.RestaurantId != restaurantId)
                {
                    Console.WriteLine($"Yemek ID={menuItemId} bu restorana aid deyil ve ya tapilmadi, atlandi.");
                    continue;
                }
                if (quantity <= 0) continue;

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = menuItemId,
                    Quantity = quantity,
                    Price = menuItem.Price   // snapshot price at time of order
                });

                total += menuItem.Price * quantity;
                menuItem.TotalSold += quantity;
                await _menuItemRepo.UpdateAsync(menuItem);
            }

            if (order.OrderItems.Count == 0)
            {
                Console.WriteLine("Hec bir etibarly yemek tapilmadi. Sifaris elave edilmedi."); return;
            }

            order.TotalAmount = total;
            await _orderRepo.AddAsync(order);

            // Update restaurant stats
            restaurant.TotalOrders++;
            restaurant.TotalRevenue += total;
            await _restaurantRepo.UpdateAsync(restaurant);

            // Update table stats
            table.OrderCount++;
            await _tableRepo.UpdateAsync(table);

            Console.WriteLine($"Sifaris elave edildi. Umumi meblег: {total:F2} AZN");
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) { Console.WriteLine("Sifaris tapilmadi."); return; }
            await _orderRepo.DeleteAsync(id);
            Console.WriteLine("Sifaris silindi.");
        }

        public async Task ListOrdersAsync(int restaurantId)
        {
            var orders = await _orderRepo.GetByRestaurantAsync(restaurantId);
            Console.WriteLine();
            Console.WriteLine($"{"ID",-5} {"Tarix",-22} {"Masa ID",8} {"Meblег (AZN)",15}");
            Console.WriteLine(new string('-', 55));
            foreach (var o in orders)
                Console.WriteLine($"{o.Id,-5} {o.OrderDate,-22} {o.TableId,8} {o.TotalAmount,15:F2}");
            Console.WriteLine();
        }
    }
}
