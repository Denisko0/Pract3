using System;
using System.IO;
using System.Linq;
using System.Text.Json;

class Program
{
    static void Main()
    {
        
        string directoryPath = "C:/products/";

        
        string category = "electronics";
        double priceLimit = 1000.0;

        
        Predicate<Product> filter = p => p.Category == category && p.Price <= priceLimit;
        Action<Product> display = p => Console.WriteLine($"{p.Name}, {p.Price:C}");

        
        for (int i = 1; i <= 10; i++)
        {
            string filePath = Path.Combine(directoryPath, $"{i}.json");
            if (!File.Exists(filePath)) continue;

            string json = File.ReadAllText(filePath);
            Product[] products = JsonSerializer.Deserialize<Product[]>(json);

            var filteredProducts = products.Where(filter);

            Console.WriteLine($"Filtered products in file {i}:");
            foreach (var product in filteredProducts)
            {
                display(product);
            }
        }
    }
}

class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}