/*
 * This is a standalone class that holds 'common' functions that
 * make console applications more lightweight and less bloated
 */

namespace ShoppingCart
{
	internal class SimpleConsoleFunctions
	{
		public static void SelectEndingAction(out bool mainLoop)
		{
			// reset loop state before entering loop
			bool tempLoopValue = false;
			bool loopEndingSelector = true;

			Console.WriteLine("Choose what happens next:");
			PrintBlank();
			Console.WriteLine("1. Restart program");
			Console.WriteLine("2. Quit program");

			while (loopEndingSelector)
			{
				ParseIntEC(out int userSelection);
				switch (userSelection)
				{
					case 1:
						loopEndingSelector = false;
						tempLoopValue = true;
						Console.Clear(); // clear screen to make room for new info
						break;

					case 2:
						loopEndingSelector = false;
						tempLoopValue = false;
						PrintBlank(); // write buffer line to keep results on-screen after program ends
						break;

					default:
						tempLoopValue = true;
						PrintInvalidSelection();
						break;
				}
			}
			// "The Out Parameter must be assigned before control leaves the current method"
			// So we just use a temp value and assign it
			// to the actual value once the switch is over
			mainLoop = tempLoopValue;

		}

		#region Parsing
		// EC = Error correction
		public static bool ParseIntEC(out int val)
		{
			return int.TryParse(Console.ReadLine(), out val);
		}

		public static bool ParseDoubleEC(out double val)
		{
			return double.TryParse(Console.ReadLine(), out val);
		}

		public static string ParseEndingInput()
		{
			Console.WriteLine("Input any key to close program...");
			// in-case of a null string
			return Console.ReadLine() ?? " ";
		}
		#endregion

		#region Printing
		public static void PrintBlank()
		{
			Console.WriteLine("");
		}

		public static void PrintValue(int val)
		{
			Console.WriteLine("Value is: " + val);
		}

		public static void PrintInvalidSelection()
		{
			Console.WriteLine("Invalid selection, please select a listed option.");
		}
		#endregion
	}
}