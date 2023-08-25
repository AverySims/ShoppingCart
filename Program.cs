using System.Collections.Immutable;

namespace ShoppingCart
{
	class Program
    {
        static List<CartItem> cart = new List<CartItem>();

        // Pre-defined items
        static CartItem[] predefinedItems = {
            new CartItem("Apple", 0.5f),
            new CartItem("Banana", 0.3f),
            new CartItem("Orange", 0.6f)
        };

        static void Main(string[] args)
        {
            RunShoppingCart();
        }

        static void RunShoppingCart()
        {
            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddItem();
                        break;
                    case "2":
                        ViewCart();
                        break;
                    case "3":
                        CalculateTotal();
                        break;
                    case "4":
                        Console.WriteLine("Exiting the program.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please choose a valid option.");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add an item to the cart");
            Console.WriteLine("2. View the current cart");
            Console.WriteLine("3. Calculate total price");
            Console.WriteLine("4. Exit");
        }

        static void AddItem()
        {
            Console.WriteLine("Pre-defined items:");
            for (int i = 0; i < predefinedItems.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {predefinedItems[i].Name} - ${predefinedItems[i].Price} each");
            }

            Console.Write("Enter the item number you want to add: ");
            int itemNumber = int.Parse(Console.ReadLine());

            int quantity;
            Console.Write("Enter item quantity: ");
            while (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Please enter a valid quantity.");
            }

            if (itemNumber >= 1 && itemNumber <= predefinedItems.Length)
            {
                CartItem selectedItem = predefinedItems[itemNumber - 1];
                CartItem existingItem = cart.FirstOrDefault(item => item.Name == selectedItem.Name);

                if (existingItem != null)
                {
                    existingItem.AddQuantity(quantity);
                    Console.WriteLine($"{quantity} {selectedItem.Name}(s) added to the cart.");
                }
                else
                {
                    cart.Add(new CartItem(selectedItem.Name, selectedItem.Price));
                    cart.Last().AddQuantity(quantity);
                    Console.WriteLine($"{quantity} {selectedItem.Name}(s) added to the cart.");
                }
            }
            else
            {
                Console.WriteLine("Invalid item number.");
            }
        }

        static void ViewCart()
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                Console.WriteLine("Current Cart:");
                foreach (var item in cart)
                {
                    Console.WriteLine($"{item.Quantity} {item.Name}(s) - ${item.Price} each");
                }
            }
        }

        static void CalculateTotal()
        {
            float total = 0;
            foreach (var item in cart)
            {
                total += item.Price * item.Quantity;
            }
            Console.WriteLine($"Total Price: ${total:F2}");
        }
    }

    class CartItem
    {
        public string Name { get; }
        public float Price { get; }
        public int Quantity { get; private set; }

        public CartItem(string name, float price)
        {
            Name = name;
            Price = price;
            Quantity = 0;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
}