using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.UI
{
    public static class OrderMenu
    {
        public static async Task ShowAsync(OrderService service)
        {
            Console.WriteLine("\n1. Elave et  2. Sil  3. Siyahi");
            Console.Write("Secim: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int rid);
                    Console.Write("Masa ID: ");
                    int.TryParse(Console.ReadLine(), out int tid);
                    var items = new Dictionary<int, int>();
                    while (true)
                    {
                        Console.Write("Yemek ID (0 - bitir): ");
                        int.TryParse(Console.ReadLine(), out int mid);
                        if (mid == 0) break;
                        Console.Write("Say: ");
                        int.TryParse(Console.ReadLine(), out int qty);
                        if (qty > 0) items[mid] = qty;
                    }
                    await service.AddOrderAsync(rid, tid, items);
                    break;
                case "2":
                    Console.Write("Sifaris ID: ");
                    int.TryParse(Console.ReadLine(), out int did);
                    await service.DeleteOrderAsync(did);
                    break;
                case "3":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int lrid);
                    await service.ListOrdersAsync(lrid);
                    break;
                default:
                    Console.WriteLine("Yanlis secim.");
                    break;
            }
        }
    }
}
