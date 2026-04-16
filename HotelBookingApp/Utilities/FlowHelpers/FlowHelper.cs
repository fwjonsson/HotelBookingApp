namespace HotelBookingApp.Utilities.FlowHelpers
{
    internal static class FlowHelper
    {
        internal static bool CheckItemExists<T>(T? item, string notFoundMessage) where T : class
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

