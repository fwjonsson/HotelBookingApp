using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class SelectCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly SelectObjectMenu<Customer> _selectCustomerMenu;

        internal SelectCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _selectCustomerMenu = new SelectObjectMenu<Customer>();
        }
        private Customer? SelectCustomer(Func<List<Customer>> getCustomers, Func<Customer, string> formatter, string title)
        {
            while (true)
            {
                List<Customer> customers = getCustomers();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForContinue();
                    return null;
                }

                Customer? customer = _selectCustomerMenu.Show(customers, formatter, title);
                if (customer == null)
                {
                    _selectCustomerMenu.ResetMenu();
                    return null;
                }
                _selectCustomerMenu.ResetMenu();
                return customer;
            }
        }
        internal Customer? ReadAllAndGetSelected()
        {
            return SelectCustomer(_customerService.ListAll, CustomerPrinter.MenuShowAllFormatCustomer,
                "--All Customers--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |   Phone Number\n" + new string('-', 103));
        }
        internal Customer? ReadByIdAndGet()
        {
            int? customerIdInput = InputHelper.ReadInt("Customer ID: ");
            if (customerIdInput is not int customerId)
                return null;
            var customer = _customerService.GetById(customerId);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                InputHelper.WaitForContinue();
            }
            return customer;
        }
        internal Customer? ReadByFullNameAndGetSelected()
        {
            string? fullNameInput = InputHelper.ReadFullName("Full Name: ");
            if (fullNameInput is not string fullName)
                return null;
            return SelectCustomer(() => _customerService.GetByFullName(fullName), CustomerPrinter.MenuShowAllFormatCustomer,
                $"--Customers By Name: {fullName}--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |   Phone Number\n" + new string('-', 103));
        }
        internal Customer? ReadByBirthDateAndGetSelected()
        {
            DateOnly? birthDateInput = InputHelper.ReadDateOnly("Birth Date: ");
            if (birthDateInput is not DateOnly birthDate)
                return null;
            return SelectCustomer(() => _customerService.GetByBirthDate(birthDate), CustomerPrinter.MenuShowAllFormatCustomer,
                $"--Customers With Birth Date: {birthDate:yyyy-MM-dd}--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |   Phone Number\n" + new string('-', 103));
        }
        internal Customer? ReadByEmailAndGet()
        {
            string? emailInput = InputHelper.ReadEmail("Email: ");
            if (emailInput is not string email)
                return null;
            var customer = _customerService.GetByEmail(email);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                InputHelper.WaitForContinue();
            }
            return customer;
        }
        internal Customer? ReadByPhoneNumberAndGet()
        {
            string? phoneNumberInput = InputHelper.ReadPhoneNumber("Phone Number: ");
            if (phoneNumberInput is not string phoneNumber)
                return null;
            var customer = _customerService.GetByPhoneNumber(phoneNumber);
            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                InputHelper.WaitForContinue();
            }
            return customer;
        }
        internal Customer? ReadByBookingIdAndGet()
        {
            int? bookingIdInput = InputHelper.ReadInt("Booking ID: ");
            if (bookingIdInput is not int bookingId)
                return null;
            var customer = _customerService.GetByBookingId(bookingId);
            if(customer == null)
            {
                Console.WriteLine("Customer not found.");
                InputHelper.WaitForContinue();
            }
            return customer;
        }
    }
}
