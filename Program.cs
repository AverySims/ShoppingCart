using System.Collections.Immutable;

namespace ShoppingCart
{
    // class defined to hold item information
    class CartItem
    {
        // constructor
        public CartItem(string name, double price)
        {
            Name = name;
            Price = price;
            Quantity = 0;
        }
        
        public string Name { get; }
        public double Price { get; }
        public int Quantity { get; private set; }
        
        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
    }
    
	class Program
    {
        // List variable where all items are stored
        public static List<CartItem> shoppingCart = new List<CartItem>();
        
        // string array that displays menu selections
        private static ImmutableArray<string> menuOptions = ImmutableArray.Create(new string[]
        { "Add item to cart",
            "View items in cart",
            "View total price",
            "Exit program" });

        // Pre-defined items, can add items when needed
        private static CartItem[] preDefinedItems = {
            new CartItem("Apple", 0.5),
            new CartItem("Banana", 0.3),
            new CartItem("Orange", 0.6)
        };
        
        // Main() is set to async so the Task.Delay() can pause the
        // Console.Writeline() functions to keep information on screen
        // longer without having to print the same lines multiple times
        static async Task Main(string[] args)
        {
            bool loopMain = true;
            // storing the user selection in a local var
            // so we can change loopMain behaviour when
            // loop is finished
            int tempInt;
            
            while (loopMain)
            {
                PrintMenu();
                tempInt = SelectMenuOption();
                // checking for program end
                if (tempInt != 4)
                {
                    await Task.Delay(2000);
                    Console.Clear();
                }
                else
                {
                    loopMain = false;
                }
            }
        }

        #region Menu
        static void PrintMenu()
        {
            Console.WriteLine("- - - Menu - - -");
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
        }

        static int SelectMenuOption()
        {
            bool loopTemp = true;
            int tempSelect = 0;
            
            while (loopTemp)
            {
                tempSelect = GenericParse.TryReadLine<int>();

                switch (tempSelect)
                {
                    case 1:
                        loopTemp = false;
                        AddItem();
                        break;
                    case 2:
                        loopTemp = false;
                        ViewCart();
                        break;
                    case 3:
                        loopTemp = false;
                        CalculateTotal();
                        break;
                    case 4:
                        loopTemp = false;
                        Console.WriteLine("Exiting program.");
                        return tempSelect;
                    default:
                        SimpleConsoleFunctions.PrintInvalidSelection();
                        break;
                }
                
                loopTemp = false;
            }
            
            return tempSelect;
        }
        #endregion

        #region Items
        private static void AddItem()
        {
            Console.Clear();
            // printing available items
            PrintItems();
            
            // user inputs item index based on PrintItems()
            Console.Write("Enter the item number: ");
            // Inline variable declaration
            SelectItemNumber(out int tempSelect);
            
            // user inputs desired quantity
            Console.Write("Enter item quantity: ");
            // Inline variable declaration
            SelectItemQuantity(out int tempQuantity);
            
            // checking shoppingCart to see if selected item is already in cart
            CartItem selectedItem = preDefinedItems[tempSelect - 1];
            CartItem existingItem = shoppingCart.FirstOrDefault(item => item.Name == selectedItem.Name);
            
            if (existingItem != null) // we change the quantity of the item IN the shoppingCart list
            {
                existingItem.AddQuantity(tempQuantity);
                Console.WriteLine($"{tempQuantity} {selectedItem.Name}(s) added to the cart.");
            }
            else // we add it to the list and change the quantity of it
            {
                shoppingCart.Add(new CartItem(selectedItem.Name, selectedItem.Price));
                shoppingCart.Last().AddQuantity(tempQuantity);
                Console.WriteLine($"{tempQuantity} {selectedItem.Name}(s) added to the cart.");
            }
        }

        private static void SelectItemNumber(out int input)
        {
            while (true)
            {
                input = GenericParse.TryReadLine<int>();
                if (input >= 1 && input <= preDefinedItems.Length)
                {
                    break;
                }
                else
                {
                    SimpleConsoleFunctions.PrintInvalidSelection();
                }
            }
        }

        private static void SelectItemQuantity(out int input)
        {
            while (true)
            {
                input = GenericParse.TryReadLine<int>();
                if (input >= 1)
                {
                    break;
                }
                else
                {
                    SimpleConsoleFunctions.PrintInvalidSelection();
                }
            }
        }
        
        private static void PrintItems()
        {
            Console.WriteLine("Available items:");
            for (int i = 0; i < preDefinedItems.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {preDefinedItems[i].Name} - ${preDefinedItems[i].Price} each");
            }
        }
        #endregion
        
        static void ViewCart()
        {
            SimpleConsoleFunctions.PrintBlank();
            
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
            }
            else
            {
                Console.WriteLine("Current Cart:");
                foreach (var item in shoppingCart)
                {
                    Console.WriteLine($"{item.Quantity} {item.Name}(s) - ${item.Price:F2} each");
                }
            }
        }

        static void CalculateTotal()
        {
            double tempTotal = 0;
            foreach (var item in shoppingCart)
            {
                tempTotal += item.Price * item.Quantity;
            }
            SimpleConsoleFunctions.PrintBlank();
            Console.WriteLine($"Total Price: ${tempTotal:F2}");
        }
    }
}