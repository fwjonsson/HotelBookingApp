using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
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

        internal void Run(int customerId)
        {
            Customer? customer = _customerService.GetById(customerId);
            FlowHelper.EnsureFound(customer, "Customer not found.");
            if (customer == null)
                return;

            CustomerPrinter.PrintCustomer(customer);
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                CustomerValidationResult result = _customerService.DeleteCustomer(customer.Id);

                if (!result.CustomerFound)
                {
                    Console.WriteLine("No customer with that ID found.");
                }
                if (!result.CanBeDeleted)
                {
                    Console.WriteLine("Cannot delete a customer with current or future bookings.");
                }
                if (result.Success)
                {
                    Console.WriteLine("Customer deleted.");
                }
            }
            InputHelper.WaitForContinue();
        }
    }
}
