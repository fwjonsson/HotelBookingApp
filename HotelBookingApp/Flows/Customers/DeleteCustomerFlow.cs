using HotelBookingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class DeleteCustomerFlow
    {
        private readonly CustomerService _customerService;


        internal DeleteCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
        }
    }
}
