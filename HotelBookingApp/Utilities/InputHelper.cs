using Microsoft.Identity.Client;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

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

                Console.WriteLine("ERROR: Must be a number. Press any key to try again.");
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

                Console.WriteLine("ERROR: Bad date format. Must be yyyy-MM-dd. Press any key to try again.");
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
        internal static void WaitForContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey(true);
        }

        internal static string ReadFullName(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
            var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Name cannot be empty.");
                    WaitForContinue();
                    continue;
                }
                return input;
            }
        }

        internal static string ReadEmail(string prompt)
        {
            var emailValidator = new EmailAddressAttribute();

            Console.WriteLine(prompt);
            while (true)
            {
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Email cannot be empty.");
                    WaitForContinue();
                    continue;
                }
                if (!emailValidator.IsValid(input))
                {
                    Console.WriteLine("Bad format.");
                    WaitForContinue();
                    continue;
                }
                return input;
            }
        }

        internal static string ReadPhoneNumber(string prompt)
        {
            var phoneNumberValidator = new PhoneAttribute();

            Console.WriteLine(prompt);

            while (true)
            {
                var input = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Phone number cannot be empty");
                    WaitForContinue();
                    continue;
                }
                if (!phoneNumberValidator.IsValid(input))
                {
                    Console.WriteLine("Bad format.");
                    WaitForContinue();
                    continue;
                }
                return input;
            }
        }
    }
}
