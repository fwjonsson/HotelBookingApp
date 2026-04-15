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
                        string? newFullNameInput = InputHelper.ReadFullName("Full Name: ");
                        if (newFullNameInput is string nFullName)
                            newFullName = nFullName;
                        break;

                    case CustomerEditorMenuOption.BirthDate:
                        DateOnly? newBirthDateInput = InputHelper.ReadDateOnly("Birth Date: ");
                        if (newBirthDateInput is DateOnly nBirthDate)
                            newBirthDate = nBirthDate;
                        break;

                    case CustomerEditorMenuOption.Email:
                        string? newEmailInput = InputHelper.ReadEmail("Email: ");
                        if (newEmailInput is string nEmailInput)
                            newEmail = nEmailInput;
                        break;

                    case CustomerEditorMenuOption.PhoneNumber:
                        string? newPhoneNumberInput = InputHelper.ReadPhoneNumber("Phone Number: ");
                        if (newPhoneNumberInput is string nPhoneNumber)
                            newPhoneNumber = nPhoneNumber;
                        break;

                    case CustomerEditorMenuOption.Save:
                        if (TrySave(customer.Id, newFullName, newBirthDate, newEmail, newPhoneNumber))
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
            InputHelper.WaitForContinue();
            if (result.Success)
                return true;
            return false;
        }
    }
}
