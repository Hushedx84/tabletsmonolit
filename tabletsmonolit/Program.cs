using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Ласкаво просимо до інтернет-магазину планшетів!");
            Console.WriteLine("1: Реєстрація користувача.");
            Console.WriteLine("2: Вибір планшета.");
            Console.WriteLine("3: Вихід.");
            Console.Write("Оберіть опцію: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterUser();
                    break;
                case "2":
                    SelectTablet();
                    break;
                case "3":
                    isRunning = false;
                    Console.WriteLine("Дякуємо за використання нашого інтернет-магазину!");
                    break;
                default:
                    Console.WriteLine("Невірний ввід. Будь ласка, оберіть коректний варіант.");
                    break;
            }
        }
    }

    static void RegisterUser()
    {
        Console.WriteLine("Введіть логін для реєстрації:");
        string username = Console.ReadLine();
        Console.WriteLine("Введіть пароль для реєстрації:");
        string password = Console.ReadLine();

        string filePath = "users.csv";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "Логін,Пароль\n");
        }
        File.AppendAllText(filePath, $"{username},{password}\n");
        Console.WriteLine("Реєстрація пройшла успішно!");
    }

    static void SelectTablet()
    {
        var tablets = LoadTabletsFromCsv("tablets.csv");
        Console.WriteLine("Доступні планшети:");
        foreach (var tablet in tablets)
        {
            Console.WriteLine($"ID: {tablet.Id}, Модель: {tablet.Model}, Виробник: {tablet.Manufacturer}, Ціна: {tablet.Price}$");
        }

        Console.WriteLine("Введіть ID планшета, який бажаєте придбати:");
        var selectedId = Console.ReadLine();
        var selectedTablet = tablets.FirstOrDefault(t => t.Id == selectedId);

        if (selectedTablet != null)
        {
            var orderNumber = GenerateOrderNumber(selectedTablet.Id);
            Console.WriteLine($"Ваш номер замовлення: {orderNumber}. Збережіть його для отримання замовлення.");
        }
        else
        {
            Console.WriteLine("Планшет з таким ID не знайдено.");
        }
    }

    static List<Tablet> LoadTabletsFromCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath).Skip(1);
        return lines.Select(line =>
        {
            var parts = line.Split(',');
            return new Tablet
            {
                Id = parts[0].Trim(),
                Model = parts[1].Trim(),
                Manufacturer = parts[2].Trim(),
                OperatingSystem = parts[3].Trim(),
                ScreenSize = parts[4].Trim(),
                ScreenResolution = parts[5].Trim(),
                RAM = parts[6].Trim(),
                Storage = parts[7].Trim(),
                Color = parts[8].Trim(),
                Price = parts[9].Trim()
            };
        }).ToList();
    }

    static string GenerateOrderNumber(string productId)
    {
        var random = new Random();
        var randomNumber = random.Next(1000, 9999);
        return $"{productId}-{randomNumber}";
    }
}

class Tablet
{
    public string Id { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public string OperatingSystem { get; set; }
    public string ScreenSize { get; set; }
    public string ScreenResolution { get; set; }
    public string RAM { get; set; }
    public string Storage { get; set; }
    public string Color { get; set; }
    public string Price { get; set; }
}
