namespace ShoppingCart
{
	public class Item
	{
		private string name;
		private double price;
		private int quantity;
		
		public Item(string name, double price, int quantity)
		{
			Name = name;
			Price = price;
			Quantity = quantity;
		}
		
		public string Name { get; }
		public double Price { get; }
		public int Quantity { get; set; }
		
	}
    
	internal class Program
	{
		private static readonly string[] menuOptions =
			{ "Add item to cart", "View items in cart", "See total price of cart", "Exit program" };
		
		private static List<Item> cart = new List<Item>();
		
		static void Main(string[] args)
		{
			bool loopMain = true;
			
			while (loopMain)
			{
				PrintSelectionMenu();
				SelectMenuOption();
				SimpleConsoleFunctions.ParseEndingInput();
			}
		}

		private static void PrintSelectionMenu()
		{
			Console.WriteLine("- - - Menu - - -");
			for (int i = 0; i < menuOptions.Length; i++)
			{
				Console.WriteLine($"{i + 1}. {menuOptions[i]}");
			}
		}

		private static void SelectMenuOption()
		{
			bool loopTemp = true;
			int tempSelect = 0;
			
			while (loopTemp)
			{
				//bool tempBool = SimpleConsoleFunctions.ParseIntEC(out tempSelect);
				
				if (!SimpleConsoleFunctions.ParseIntEC(out tempSelect) || (tempSelect > 0 || tempSelect < menuOptions.Length + 1))
				{
					loopTemp = false;
				}
				else
				{
					SimpleConsoleFunctions.PrintInvalidSelection();
				}
		
			}

			switch (tempSelect)
			{
				case 1: // Add item to cart
					Console.WriteLine("Adding item");
					break;
				case 2: // View items in cart
					
					break;
				case 3: // See total price of cart
					
					break;
				case 4: // Exit program
					
					break;
				
				default:
					SimpleConsoleFunctions.PrintInvalidSelection();
					break;
			}
		}

		public static void AddItemToCart()
		{
			bool loopPrice = true;
			
			string tempName;
			double tempPrice;
			int tempQuantity;
			Console.WriteLine("Item name: ");
			tempName = Console.ReadLine() ?? "null";
			
			Console.WriteLine("Item price: ($)");
			while (loopPrice)
			{
				if (!SimpleConsoleFunctions.ParseDoubleEC(out tempPrice) || tempPrice > -0)
				{
					
				}
			}
			
			
		}
	}
}