
namespace ShoppingCartApp
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
}
