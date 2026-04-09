using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services.Customers
{
    internal class CustomerValidationResult
    {
        public bool EmailAvailable { get; set; } = true;
        public bool PhoneNumberAvailable { get; set; } = true;

        public bool Success => 
            EmailAvailable && 
            PhoneNumberAvailable;
    }
}
