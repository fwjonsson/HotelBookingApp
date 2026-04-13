using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;

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
        internal void Run(int customerId)
        {
            Customer? customer = _customerService.GetById(customerId);
            if (!FlowHelper.CheckItemExistsAndWait(customer, "Customer not found"))
                return;

            string newFullName = customer!.FullName;
            DateOnly newBirthDate = customer.BirthDate;
            string newEmail = customer.Email;
            string newPhoneNumber = customer.PhoneNumber;

            while (true)
            {
                CustomerEditorMenuOption choice = _updateCustomerMenu.Show(newFullName, newBirthDate, newEmail, newPhoneNumber);

                switch (choice)
                {
                    case CustomerEditorMenuOption.FullName:
                        newFullName = InputHelper.ReadFullName("New full name: ");
                        break;
                    case CustomerEditorMenuOption.BirthDate:
                        newBirthDate = InputHelper.ReadDateOnly("New birth date: ");
                        break;
                    case CustomerEditorMenuOption.Email:
                        newEmail = InputHelper.ReadEmail("New email: ");
                        break;
                    case CustomerEditorMenuOption.PhoneNumber:
                        newPhoneNumber = InputHelper.ReadPhoneNumber("New phone number: ");
                        break;
                    case CustomerEditorMenuOption.Save:
                        bool saved = TrySave(customer.Id, newFullName, newBirthDate, newEmail, newPhoneNumber);
                        if (saved)
                            return;
                        break;
                    case CustomerEditorMenuOption.Cancel:
                        _updateCustomerMenu.ResetIndex();
                        return;
                }
            }
        }

        private bool TrySave(int customerId, string newFullName, DateOnly newBirthDate, string newEmail, string newPhoneNumber)
        {
            CustomerValidationResult result = _customerService.UpdateCustomer(customerId, newFullName, newBirthDate, newEmail, newPhoneNumber);
            if (!result.CustomerFound)
            {
                Console.WriteLine("Customer not found.");
            }
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
                Console.WriteLine("Changes saved.");
                _updateCustomerMenu.ResetIndex();
                InputHelper.WaitForContinue();
                return true;
            }
            InputHelper.WaitForContinue();
            return false;
        }
    }
}
