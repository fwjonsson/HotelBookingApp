using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Services.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class UpdateCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly UpdateCustomerMenu _updateCustomerMenu;


        internal UpdateCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _updateCustomerMenu = new UpdateCustomerMenu();
        }
        internal void Run()
        {
            var customerId
        }
    }
}
