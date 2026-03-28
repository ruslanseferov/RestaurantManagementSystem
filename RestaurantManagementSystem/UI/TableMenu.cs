using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.UI
{
    public static class TableMenu
    {
        public static async Task ShowAsync(TableService service)
        {
            Console.WriteLine("\n1. Elave et  2. Yenile  3. Sil  4. Siyahi");
            Console.Write("Secim: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int rid);
                    Console.Write("Masa No: ");
                    int.TryParse(Console.ReadLine(), out int tn);
                    Console.Write("Tutum: ");
                    int.TryParse(Console.ReadLine(), out int cap);
                    await service.AddTableAsync(rid, tn, cap);
                    break;
                case "2":
                    Console.Write("Masa ID: ");
                    int.TryParse(Console.ReadLine(), out int uid);
                    Console.Write("Yeni masa No: ");
                    int.TryParse(Console.ReadLine(), out int utn);
                    Console.Write("Yeni tutum: ");
                    int.TryParse(Console.ReadLine(), out int ucap);
                    await service.UpdateTableAsync(uid, utn, ucap);
                    break;
                case "3":
                    Console.Write("Masa ID: ");
                    int.TryParse(Console.ReadLine(), out int did);
                    await service.DeleteTableAsync(did);
                    break;
                case "4":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int lrid);
                    await service.ListTablesAsync(lrid);
                    break;
                default:
                    Console.WriteLine("Yanlis secim.");
                    break;
            }
        }
    }
}
