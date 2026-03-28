using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.UI
{
    public static class MenuItemMenu
    {
        public static async Task ShowAsync(MenuItemService service)
        {
            Console.WriteLine("\n1. Elave et  2. Yenile  3. Sil  4. Siyahi");
            Console.Write("Secim: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int rid);
                    Console.Write("Ad: "); var name = Console.ReadLine() ?? "";
                    Console.Write("Qiymet: ");
                    decimal.TryParse(Console.ReadLine(), out decimal price);
                    Console.Write("Kateqoriya: "); var cat = Console.ReadLine() ?? "";
                    await service.AddMenuItemAsync(rid, name, price, cat);
                    break;
                case "2":
                    Console.Write("ID: ");
                    int.TryParse(Console.ReadLine(), out int uid);
                    Console.Write("Yeni ad: "); var uname = Console.ReadLine() ?? "";
                    Console.Write("Yeni qiymet: ");
                    decimal.TryParse(Console.ReadLine(), out decimal uprice);
                    Console.Write("Yeni kateqoriya: "); var ucat = Console.ReadLine() ?? "";
                    await service.UpdateMenuItemAsync(uid, uname, uprice, ucat);
                    break;
                case "3":
                    Console.Write("ID: ");
                    int.TryParse(Console.ReadLine(), out int did);
                    await service.DeleteMenuItemAsync(did);
                    break;
                case "4":
                    Console.Write("Restoran ID: ");
                    int.TryParse(Console.ReadLine(), out int lrid);
                    await service.ListMenuItemsAsync(lrid);
                    break;
                default:
                    Console.WriteLine("Yanlis secim.");
                    break;
            }
        }
    }
}
