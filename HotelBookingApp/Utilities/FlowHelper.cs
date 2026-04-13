using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal static class FlowHelper
    {
        internal static void EnsureFound<T>(T? item, string notFoundMessage) where T : class
        {
            if (item == null)
            {
                Console.WriteLine(notFoundMessage);
                InputHelper.WaitForContinue();
                return;
            }
            return;
        }
    }
}
