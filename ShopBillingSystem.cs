// Shop Billing System - C# Console Application

using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopBillingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BillingSystem system = new BillingSystem();
            system.Run();
        }
    }

    public class BillingSystem
    {
        private List<Item> cart = new List<Item>();
        private Dictionary<string, Item> inventory = new Dictionary<string, Item>();

        public BillingSystem()
        {
            // Sample inventory
            inventory.Add("101", new Item("101", "Milk", 30));
            inventory.Add("102", new Item("102", "Bread", 25));
            inventory.Add("103", new Item("103", "Eggs", 5));
        }

        public void Run()
        {
            Console.WriteLine("\n=== Welcome to Shop Billing System ===\n");
            string choice;
            do
            {
                Console.WriteLine("1. View Inventory\n2. Add Item to Cart\n3. View Cart\n4. Generate Bill\n5. Exit");
                Console.Write("Select an option: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewInventory(); break;
                    case "2": AddToCart(); break;
                    case "3": ViewCart(); break;
                    case "4": GenerateBill(); break;
                    case "5": Console.WriteLine("Exiting..."); break;
                    default: Console.WriteLine("Invalid option\n"); break;
                }

            } while (choice != "5");
        }

        private void ViewInventory()
        {
            Console.WriteLine("\n--- Inventory ---");
            foreach (var item in inventory.Values)
            {
                Console.WriteLine($"Code: {item.Code}, Name: {item.Name}, Price: ₹{item.Price}");
            }
        }

        private void AddToCart()
        {
            Console.Write("Enter Item Code: ");
            var code = Console.ReadLine();
            if (inventory.ContainsKey(code))
            {
                Console.Write("Enter Quantity: ");
                if (int.TryParse(Console.ReadLine(), out int qty))
                {
                    var selected = inventory[code];
                    cart.Add(new Item(selected.Code, selected.Name, selected.Price, qty));
                    Console.WriteLine("Item added to cart.\n");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.\n");
                }
            }
            else
            {
                Console.WriteLine("Item not found in inventory.\n");
            }
        }

        private void ViewCart()
        {
            Console.WriteLine("\n--- Cart ---");
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.\n");
                return;
            }

            foreach (var item in cart)
            {
                Console.WriteLine($"{item.Name} x {item.Quantity} = ₹{item.Price * item.Quantity}");
            }
        }

        private void GenerateBill()
        {
            Console.WriteLine("\n--- Final Bill ---");
            double total = 0;
            foreach (var item in cart)
            {
                double lineTotal = item.Price * item.Quantity;
                total += lineTotal;
                Console.WriteLine($"{item.Name} x {item.Quantity} = ₹{lineTotal}");
            }
            Console.WriteLine($"Total Amount: ₹{total}\n");
            cart.Clear();
        }
    }

    public class Item
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Item(string code, string name, double price, int quantity = 1)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
