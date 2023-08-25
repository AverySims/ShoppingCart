namespace ShoppingCart
{
	public class GenericParse
	{
		public static T TryReadLine<T>()
		{
			while (true)
			{
				try
				{
					T value;
					if (AttemptParse(Console.ReadLine(), out value))
					{
						return value;
					}
				}
				catch (FormatException)
				{
					Console.WriteLine("Invalid input format. Please try again.");
				}

				Console.WriteLine("Invalid input. Please try again.");
			}
		}

		private static bool AttemptParse<T>(string input, out T result)
		{
			try
			{
				result = (T)Convert.ChangeType(input, typeof(T));
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}
	}
}

