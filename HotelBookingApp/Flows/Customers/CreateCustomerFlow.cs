using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Customers;

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
        internal Customer? Run()
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
                    case CustomerEditorMenuOption.FullName:
                        fullName = InputHelper.ReadFullName("Full Name: ");
                        break;

                    case CustomerEditorMenuOption.BirthDate:
                        birthDate = InputHelper.ReadDateOnly("Birth Date: ");
                        break;

                    case CustomerEditorMenuOption.Email:
                        email = InputHelper.ReadEmail("Email: ");
                        break;

                    case CustomerEditorMenuOption.PhoneNumber:
                        phoneNumber = InputHelper.ReadPhoneNumber("Phone Number: ");
                        break;

                    case CustomerEditorMenuOption.Save:
                        (bool saved, Customer? createdCustomer) = TrySave(fullName, birthDate, email, phoneNumber);
                        if (saved)
                        {
                            _createCustomerMenu.ResetMenu();
                                return createdCustomer;
                        }
                        break;

                    case CustomerEditorMenuOption.Cancel:
                        _createCustomerMenu.ResetMenu();
                        return null;
                }
            }
        }

        private (bool,Customer?) TrySave(string? fullName, DateOnly? birthDate, string? email, string? phoneNumber)
        {
            if (fullName is not string fName ||
                birthDate is not DateOnly bDate ||
                email is not string e ||
                phoneNumber is not string pNumber)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
                return (false, null);
            }
            (CustomerValidationResult result, Customer? createdCustomer) = _customerService.CreateCustomer(fName, bDate, e, pNumber);
            CustomerValidationResultPrinter.PrintErrorsOrSuccess(result, "Customer created.");
            InputHelper.WaitForContinue();
            if (result.Success)
                return (true, createdCustomer);
            return (false, null);
        }
    }
}
