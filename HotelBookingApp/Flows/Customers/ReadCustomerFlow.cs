using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class ReadCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly ReadCustomerMenu _readCustomerMenu;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _readCustomerMenu = new ReadCustomerMenu();
        }

        internal void Run()
        {
            while (true)
            {
                ReadCustomerMenuOption choice = _readCustomerMenu.Show();

                switch (choice)
                {
                    case ReadCustomerMenuOption.ReadAll:
                        ReadAll();
                        break;
                    case ReadCustomerMenuOption.ReadById:
                        ReadById();
                        break;
                    case ReadCustomerMenuOption.ReadByFullName:
                        ReadByFullName();
                        break;
                    case ReadCustomerMenuOption.ReadByEmail:
                        ReadByEmail();
                        break;
                    case ReadCustomerMenuOption.ReadByPhoneNumber:
                        ReadByPhoneNumber();
                        break;
                    case ReadCustomerMenuOption.ReadByBookingId:
                        ReadByBookingId();
                        break;
                    case ReadCustomerMenuOption.Back:
                        _readCustomerMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }
        private void ReadAll()
        {
            List<Customer> allCustomers = _customerService.ListAll();

            CustomerPrinter.PrintCustomers(allCustomers);
        }
        private void ReadById()
        {
            var customerId = InputHelper.ReadInt("Enter customer ID: ");
            Customer? customer = _customerService.GetById(customerId);

            if (customer == null)
            {
                Console.WriteLine("No customer with that ID found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer); //Make sure EF brings correct bookings
        }
        private void ReadByFullName()
        {
            var fullName = InputHelper.ReadFullName("Full name: ");
            List<Customer> customer = _customerService.GetByFullName(fullName);

            CustomerPrinter.PrintCustomers(customer);
        }
        private void ReadByEmail()
        {
            var email = InputHelper.ReadEmail("Email: ");
            Customer? customer = _customerService.GetByEmail(email);

            if (customer == null)
            {
                Console.WriteLine("No customer with that email found.");
                return;
            }

            CustomerPrinter.PrintCustomerAndTheirBookings(customer); //Make sure EF brings correct bookings
        }
        private void ReadByPhoneNumber()
        {
            var phoneNumber = InputHelper.ReadPhoneNumber("Phone number: ");
            Customer? customer = _customerService.GetByPhoneNumber(phoneNumber);

            if (customer == null)
            {
                Console.WriteLine("No customer with that phone number found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer); //Make sure EF brings correct bookings
        }
        private void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Booking ID: ");
            Customer? customer = _customerService.GetByBookingId(bookingId);

            if (customer == null)
            {
                Console.WriteLine("No customer with that booking ID found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer); //Make sure EF brings correct bookings
        }
    }
}
