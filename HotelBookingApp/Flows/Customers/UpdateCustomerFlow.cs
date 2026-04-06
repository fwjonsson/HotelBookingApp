using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class UpdateCustomerFlow
    {
        private readonly CustomerService _customerService;


        internal UpdateCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
