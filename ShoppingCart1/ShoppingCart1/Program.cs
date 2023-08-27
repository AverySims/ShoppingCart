using CustomConsole;
using GenericParse;

namespace ShoppingCartApp
{
	class Program
	{
		// List variable where all items are stored
		public static List<CartItem> ShoppingCart = new();
		
		// Pre-defined items, can add items when needed
		public static CartItem[] PreDefinedItems = {
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
		
		// Main() is set to async so the Task.Delay() can pause the
		// Console.Writeline() functions to keep information on screen
		// longer without having to print the same lines multiple times
		static async Task Main(string[] args)
		{
			bool loopMain = true;
			// storing the user selection in a local var so we can
			// change loopMain behaviour when the loop is finished
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
			for (int i = 0; i < MenuOptions.Length; i++)
			{
				Console.WriteLine($"{i + 1}. {MenuOptions[i]}");
			}
		}

		static int SelectMenuOption()
		{
			bool loopTemp = true;
			int tempSelect = 0;

			while (loopTemp)
			{
				tempSelect = GenericReadLine.TryReadLine<int>();

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
						ConsoleHelper.PrintInvalidSelection();
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
			CartItem selectedItem = PreDefinedItems[tempSelect - 1];
			CartItem existingItem = ShoppingCart.FirstOrDefault(item => item.Name == selectedItem.Name);

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
				if (input >= 1 && input <= PreDefinedItems.Length)
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
			for (int i = 0; i < PreDefinedItems.Length; i++)
			{
				Console.WriteLine($"{i + 1}. {PreDefinedItems[i].Name} - ${PreDefinedItems[i].Price} each");
			}
		}
		#endregion

		static void ViewCart()
		{
			if (ShoppingCart.Count == 0)
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