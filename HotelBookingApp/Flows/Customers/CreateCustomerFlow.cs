using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;

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
                        bool saved = TrySave(fullName, birthDate, email, phoneNumber);
                        InputHelper.WaitForContinue();
                        if (saved)
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
                return false;
            }
            CustomerValidationResult result = _customerService.CreateCustomer(fName, bDate, e, pNumber);
            if (!result.EmailAvailable)
                Console.WriteLine("Email is already registered.");
            if (!result.PhoneNumberAvailable)
                Console.WriteLine("Phone number is already registered.");
            if (result.Success)
            {
                Console.WriteLine("Customer created successfully.");
                return true;
            }
            return false;
        }
    }
}
