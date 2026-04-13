using HotelBookingApp.Enums;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Rooms;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Utilities
{
    internal static class FlowHelper
    {
        internal static bool CheckItemExistsAndWait<T>(T? item, string notFoundMessage) where T : class
        {
            if (item == null)
            {
                Console.WriteLine(notFoundMessage);
                InputHelper.WaitForContinue();
                return false;
            }
            return true;
        }
        
    }
}

