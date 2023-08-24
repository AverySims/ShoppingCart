namespace ShoppingCart
{
	public class Item
	{
		public Item(string name, double price)
		{
			Name = name;
			Price = price;
		}
		public string Name { get; }
		public double Price { get; }
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
				Console.WriteLine("- - - Menu - - -");
				for (int i = 0; i < menuOptions.Length; i++)
				{
					Console.WriteLine($"{i + 1}. {menuOptions[i]}");
				}

				Console.ReadLine();
			}
		}
		
		
		
	}
}