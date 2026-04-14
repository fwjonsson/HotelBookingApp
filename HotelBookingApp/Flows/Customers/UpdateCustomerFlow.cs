using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Customers;

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
            if (!FlowHelper.CheckItemExists(customer, "Customer not found"))
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
                        newFullName = InputHelper.ReadFullName("Full name: ");
                        break;
                    case CustomerEditorMenuOption.BirthDate:
                        newBirthDate = InputHelper.ReadDateOnly("Birth date: ");
                        break;
                    case CustomerEditorMenuOption.Email:
                        newEmail = InputHelper.ReadEmail("Email: ");
                        break;
                    case CustomerEditorMenuOption.PhoneNumber:
                        newPhoneNumber = InputHelper.ReadPhoneNumber("Phone number: ");
                        break;
                    case CustomerEditorMenuOption.Save:
                        bool saved = TrySave(customer.Id, newFullName, newBirthDate, newEmail, newPhoneNumber);
                        InputHelper.WaitForContinue();
                        if (saved)
                        {
                            _updateCustomerMenu.ResetIndex();
                            return;
                        }
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
            CustomerValidationResultPrinter.PrintErrorsOrSuccess(result, "Changes saved");
            if (result.Success)
                return true;
            return false;
        }
    }
}
