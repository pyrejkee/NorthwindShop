using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NorthwindShop.Web.ViewModels;

namespace NorthwindWebApiClient
{
    class Program
    {
        private static HttpClient _client = new HttpClient();

        static void Main(string[] args)
        {
            ConfigureHttpClient();

            var products = GetAll<ProductViewModel>("api/products").GetAwaiter().GetResult();
            var categories = GetAll<CategoryViewModel>("api/categories").GetAwaiter().GetResult();

            ShowProducts(products);
            ShowCategories(categories);

            Console.ReadLine();
        }

        public static void ConfigureHttpClient()
        {
            _client.BaseAddress = new Uri("http://localhost:48000/");
        }

        public static async Task<List<T>> GetAll<T>(string path)
        {
            var response = await _client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<List<T>>(content);

                return items;
            }

            throw new Exception("Something went wrong");
        }

        public static void ShowProducts(List<ProductViewModel> products)
        {
            ConfigureConsoleColore();

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id} {product.Name}");
            }

            ConfigureConsoleSpaces();
        }

        private static void ShowCategories(List<CategoryViewModel> categories)
        {
            ConfigureConsoleColore();

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id} {category.Description}");
            }

            ConfigureConsoleSpaces();
        }

        private static void ConfigureConsoleColore()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("List of products\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        private static void ConfigureConsoleSpaces()
        {
            Console.WriteLine("\n\n");
        }
    }
}
