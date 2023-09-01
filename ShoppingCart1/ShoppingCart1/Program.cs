using CustomConsole;
using GenericParse;

namespace ShoppingCartApp
{
	class Program
	{
		// List variable where all items are stored
		public static List<CartItem> ShoppingCart = new();
		
		// Pre-defined items, can add items when needed
		public static List<CartItem> PreDefinedItems = new() {
			new("Apple", 0.5),
			new("Banana", 0.3),
			new("Orange", 0.6),
			new("Mango", 0.8),
			new("Pear", 0.7),
			new("Toothbrush", 0.2)
		};
		
		// string array that displays menu selections
		private static readonly string[] MenuOptions =
		{ "Add item to cart",
			"View items in cart",
			"View total price",
			"Exit program"
		};

		private static bool _loopMain = true;

		static void Main(string[] args)
		{
			// getting array length to find the range of selectable menu options
			int menuOptionCount = MenuOptions.Length;

			while (_loopMain)
			{
				Console.Clear();
				
				PrintMenu();

				SelectMenuOption(menuOptionCount);
			}
		}

		#region Menu
		static void PrintMenu()
		{
			Console.WriteLine("Shopping Cart App");
			ConsoleHelper.PrintStrings(MenuOptions);
		}

		static void SelectMenuOption(int menuOptions)
		{
			while (true)
			{
				ConsoleHelper.PrintBlank();
				Console.Write("Select option: ");
				if (!SwitchOnMenuSelection(GenericReadLine.TryReadLine<int>()))
				{
					break;
				}
			}
		}

		static bool SwitchOnMenuSelection(int selection)
		{
			bool tempReturnValue = true;

			// clearing console and printing menu again to prevent clutter
			Console.Clear();
			PrintMenu();
			ConsoleHelper.PrintBlank();
			switch (selection)
			{
				case 1: // Add item to cart
					AddItem();
					break;
				case 2: // View items in cart
					ViewCart();
					break;
				case 3: // View total price
					CalculateTotal();
					break;
				case 4: // Exit program
					tempReturnValue = false;
					_loopMain = false;
					Console.WriteLine("Exiting program.");
					break;
				default: // Invalid selection
					ConsoleHelper.PrintInvalidSelection();
					break;
			}
			// returning true will keep the program running, false will exit the program
			return tempReturnValue;
		}
		#endregion

		#region Items
		private static void AddItem()
		{
			// clearing console and printing items again to prevent clutter
			Console.Clear();
			PrintItems();

			// user inputs desired item
			Console.Write("Enter the item number: ");
			// Inline variable declaration
			SelectItemNumber(out int tempSelect);

			// user inputs desired quantity
			Console.Write("Enter item quantity: ");
			// Inline variable declaration
			SelectItemQuantity(out int tempQuantity);

			// checking shoppingCart to see if selected item is already in cart
			CartItem selectedItem = PreDefinedItems[tempSelect - 1];
			CartItem existingItem = ShoppingCart.FirstOrDefault(item => item.Name == selectedItem.Name);

			// clearing console and printing menu again to prevent clutter
			Console.Clear();
			PrintMenu();
			
			ConsoleHelper.PrintBlank();
			
			if (existingItem != null) // we change the quantity of the item IN the shoppingCart list
			{
				existingItem.AddQuantity(tempQuantity);
				Console.WriteLine($"{tempQuantity} {selectedItem.Name}(s) added to the cart.");
			}
			else // we add it to the list and change the quantity of it
			{
				ShoppingCart.Add(new CartItem(selectedItem.Name, selectedItem.Price));
				ShoppingCart.Last().AddQuantity(tempQuantity);
				Console.WriteLine($"{tempQuantity} {selectedItem.Name}(s) added to the cart.");
			}
		}

		private static void SelectItemNumber(out int input)
		{
			while (true)
			{
				input = GenericReadLine.TryReadLine<int>();
				if (input > 0 && input <= PreDefinedItems.Count)
				{
					break;
				}
				else
				{
					ConsoleHelper.PrintInvalidSelection();
				}
			}
		}

		private static void SelectItemQuantity(out int input)
		{
			while (true)
			{
				input = GenericReadLine.TryReadLine<int>();
				if (input >= 1)
				{
					break;
				}
				else
				{
					ConsoleHelper.PrintInvalidSelection();
				}
			}
		}

		private static void PrintItems()
		{
			Console.WriteLine("Available items:");
			for (int i = 0; i < PreDefinedItems.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {PreDefinedItems[i].Name} - ${PreDefinedItems[i].Price} each");
			}
			ConsoleHelper.PrintBlank();
		}
		#endregion

		static void ViewCart()
		{
			if (ShoppingCart.Count < 1)
			{
				Console.WriteLine("Your cart is empty.");
			}
			else
			{
				Console.WriteLine("Current Cart:");
				foreach (var item in ShoppingCart)
				{
					Console.WriteLine($"{item.Quantity} {item.Name}(s) - ${item.Price:F2} each");
				}
			}
		}

		static void CalculateTotal()
		{
			double tempTotal = 0;
			foreach (var item in ShoppingCart)
			{
				tempTotal += item.Price * item.Quantity;
			}
			Console.WriteLine($"Total Price: ${tempTotal:F2}");
		}
	}
}