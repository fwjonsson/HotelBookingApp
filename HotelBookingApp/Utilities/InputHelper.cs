using System.Globalization;

namespace HotelBookingApp.Utilities
{
    internal static class InputHelper
    {
        internal static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out var value)) return value;

                Console.WriteLine("ERROR: Must be a number. Press any key to try again");
                Console.ReadKey(true);
            }
        }

        internal static DateOnly ReadDateOnly(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var value)) return value;

                Console.WriteLine("ERROR: Bad date format. Must be yyyy-MM-dd. Press any key to try again");
                Console.ReadKey(true);
            }
        }
        internal static bool WaitForYesOrNo(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Y:
                        return true;
                    case ConsoleKey.N:
                        return false;
                }
            }
        }
        internal static void WaitForBack()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to go back");
            Console.ReadKey(true);
        }

    }
}
