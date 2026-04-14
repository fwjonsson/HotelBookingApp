using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
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
                    case CustomerEditorMenuOption.FullName:
                        fullName = InputHelper.ReadFullName("Full name: ");
                        break;
                    case CustomerEditorMenuOption.BirthDate:
                        birthDate = InputHelper.ReadDateOnly("Birth date (yyyy-MM-dd): ");
                        break;
                    case CustomerEditorMenuOption.Email:
                        email = InputHelper.ReadEmail("Email: ");
                        break;
                    case CustomerEditorMenuOption.PhoneNumber:
                        phoneNumber = InputHelper.ReadPhoneNumber("Phone number: ");
                        break;
                    case CustomerEditorMenuOption.Save:
                        if (TrySave(fullName, birthDate, email, phoneNumber))
                        {
                            _createCustomerMenu.ResetIndex();
                            return;
                        }
                        break;
                    case CustomerEditorMenuOption.Cancel:
                        _createCustomerMenu.ResetIndex();
                        return;
                }
            }
        }

        private bool TrySave(string? fullName, DateOnly? birthDate, string? email, string? phoneNumber)
        {
            if (fullName is not string fName ||
                birthDate is not DateOnly bDate ||
                email is not string e ||
                phoneNumber is not string pNumber)
            {
                Console.WriteLine("You must fill in all values before creating.");
                InputHelper.WaitForContinue();
                return false;
            }
            CustomerValidationResult result = _customerService.CreateCustomer(fName, bDate, e, pNumber);
            CustomerValidationResultPrinter.PrintErrorsOrSuccess(result, "Customer created successfully.");
            InputHelper.WaitForContinue();
            if (result.Success)
                return true;
            return false;
        }
    }
}
