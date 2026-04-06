using HotelBookingApp.Menus;
using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class CreateCustomerFlow
    {
        private readonly CustomerService _customerService;

        internal CreateCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
