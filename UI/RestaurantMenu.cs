using System;
using System.Threading.Tasks;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.UI
{
    public static class RestaurantMenu
    {
        public static async Task ShowAsync(RestaurantService service)
        {
            Console.WriteLine("\n1. Elave et  2. Yenile  3. Sil  4. Siyahi");
            Console.Write("Secim: ");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Write("Ad: "); var name = Console.ReadLine() ?? "";
                    Console.Write("Filial kodu (1-99): ");
                    int.TryParse(Console.ReadLine(), out int bc);
                    await service.AddRestaurantAsync(name, bc);
                    break;
                case "2":
                    Console.Write("ID: ");
                    int.TryParse(Console.ReadLine(), out int uid);
                    Console.Write("Yeni ad: "); var uname = Console.ReadLine() ?? "";
                    Console.Write("Yeni filial kodu: ");
                    int.TryParse(Console.ReadLine(), out int ubc);
                    await service.UpdateRestaurantAsync(uid, uname, ubc);
                    break;
                case "3":
                    Console.Write("ID: ");
                    int.TryParse(Console.ReadLine(), out int did);
                    await service.DeleteRestaurantAsync(did);
                    break;
                case "4":
                    await service.ListRestaurantsAsync();
                    break;
                default:
                    Console.WriteLine("Yanlis secim.");
                    break;
            }
        }
    }
}
