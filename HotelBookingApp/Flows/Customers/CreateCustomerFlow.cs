using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class CreateCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly CreateCustomerMenu _createCustomerMenu;

        internal CreateCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _createCustomerMenu = new CreateCustomerMenu();
        }
        internal void Run()
        {
            string? fullName = null;
            DateOnly? birthDate = null;
            string? email = null;
            string? phoneNumber = null;
            

            while (true)
            {
                var choice = _createCustomerMenu.Show(fullName, birthDate, email, phoneNumber);
                switch (choice)
                {
                    case CreateCustomerMenuOption.FullName:
                        fullName = InputHelper.ReadFullName("Enter customers full name: ");
                        break;
                    case CreateCustomerMenuOption.Birthdate:
                        birthDate = InputHelper.ReadDateOnly("Enter customers birth date (yyyy-MM-dd): ");
                        break;
                    case CreateCustomerMenuOption.Email:
                        email = InputHelper.ReadEmail("Enter customer email: ");
                        break;
                    case CreateCustomerMenuOption.PhoneNumber:
                        phoneNumber = InputHelper.ReadPhoneNumber("Enter customer phone number: ");
                        break;
                    case CreateCustomerMenuOption.Create:
                        if (fullName is not string fName ||
                            birthDate is not DateOnly bDate ||
                            email is not string e ||
                            phoneNumber is not string pNumber)
                        {
                            Console.WriteLine("You must fill in all values before creating.");
                            InputHelper.WaitForContinue();
                            continue;
                        }
                        CustomerValidationResult result = _customerService.CreateCustomer(fName, bDate, e, pNumber);
                        if (!result.EmailAvailable)
                        {
                            Console.WriteLine("Email is already registered.");
                        }
                        if (!result.PhoneNumberAvailable)
                        {
                            Console.WriteLine("Phone number is already registered.");
                        }
                        if (result.Success)
                        {
                            Console.WriteLine("Customer created successfully.");
                            _createCustomerMenu.ResetIndex();
                            InputHelper.WaitForContinue();
                            return;
                        }
                        break;
                    case CreateCustomerMenuOption.Cancel:
                        _createCustomerMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }
    }
}
