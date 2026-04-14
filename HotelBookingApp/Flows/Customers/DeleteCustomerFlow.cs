using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Customers;
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

        internal bool TryDelete(int customerId)
        {
            if (InputHelper.WaitForYesOrNo("Are you sure you want to delete? Y/N"))
            {
                Customer? customer = _customerService.GetById(customerId);
                CustomerValidationResult result = _customerService.DeleteCustomer(customer!.Id);
                CustomerValidationResultPrinter.PrintErrorsOrSuccess(result, "Customer deleted.");
                InputHelper.WaitForContinue();
                if (result.Success)
                    return true;
            }
            return false;
        }
    }
}
