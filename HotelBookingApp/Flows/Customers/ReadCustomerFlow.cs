using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class ReadCustomerFlow
    {
        private readonly CustomerService _customerService;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
