using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Menus;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Customers;

namespace HotelBookingApp.Flows.Customers
{
    internal class ReadCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly SelectObjectMenu<Customer> _selectCustomerMenu;
        private readonly ActionMenu<Customer> _customerActionMenu;
        private readonly UpdateCustomerFlow _updateCustomerFlow;
        private readonly DeleteCustomerFlow _deleteCustomerFlow;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _selectCustomerMenu = new SelectObjectMenu<Customer>(CustomerPrinter.MenuFormatCustomer);
            _customerActionMenu = new ActionMenu<Customer>("--Customer--", CustomerPrinter.FormatCustomer);
            _updateCustomerFlow = new UpdateCustomerFlow(customerService);
            _deleteCustomerFlow = new DeleteCustomerFlow(customerService);
        }

        private void SelectCustomerAndChooseAction(Func<List<Customer>> getCustomers, string title)
        {
            while (true)
            {
                List<Customer> customers = getCustomers();

                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForContinue();
                    return;
                }

                Customer? customer = _selectCustomerMenu.Show(customers, title);
                if (customer == null)
                {
                    _selectCustomerMenu.ResetIndex();
                    return;
                }

                ChooseAction(_customerService.GetById, customer.Id);
            }
        }
        private void ChooseAction<TId>(Func<TId, Customer?> getCustomer, TId identifier)
        {
            while (true)
            {
                Customer? customer = getCustomer(identifier);
                if (!FlowHelper.CheckItemExists(customer, "Customer not found."))
                    return;

                ActionMenuOption choice = _customerActionMenu.Show(customer!);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateCustomerFlow.Run(customer!.Id);
                        break;

                    case ActionMenuOption.Delete:
                        if (_deleteCustomerFlow.TryDelete(customer!.Id))
                        {
                            _customerActionMenu.ResetIndex();
                            return;
                        }
                        break;

                    case ActionMenuOption.Back:
                        _customerActionMenu.ResetIndex();
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            SelectCustomerAndChooseAction(_customerService.ListAll, "--All Customers--");
        }
        internal void ReadById()
        {
            int? customerIdInput = InputHelper.ReadInt("Customer ID: ");
            if (customerIdInput is not int customerId)
                return;
            ChooseAction(_customerService.GetById, customerId);
        }
        internal void ReadByFullName()
        {
            string? fullNameInput = InputHelper.ReadFullName("Full Name: ");
            if (fullNameInput is not string fullName)
                return;
            SelectCustomerAndChooseAction(() => _customerService.GetByFullName(fullName), $"--Customers By Name: {fullName}--");
        }
        internal void ReadByBirthDate()
        {
            DateOnly? birthDateInput = InputHelper.ReadDateOnly("Birth Date: ");
            if (birthDateInput is not DateOnly birthDate)
                return;
            SelectCustomerAndChooseAction(() => _customerService.GetByBirthDate(birthDate), $"--Customers With Birth Date: {birthDate:yyyy-MM-dd}--");
        }
        internal void ReadByEmail()
        {
            string? emailInput = InputHelper.ReadEmail("Email: ");
            if (emailInput is not string email)
                return;
            ChooseAction(_customerService.GetByEmail, email);
        }
        internal void ReadByPhoneNumber()
        {
            string? phoneNumberInput = InputHelper.ReadPhoneNumber("Phone Number: ");
            if (phoneNumberInput is not string phoneNumber)
                return;
            ChooseAction(_customerService.GetByPhoneNumber, phoneNumber);
        }
        internal void ReadByBookingId()
        {
            int? bookingIdInput = InputHelper.ReadInt("Booking ID: ");
            if (bookingIdInput is not int bookingId)
                return;
            ChooseAction(_customerService.GetByBookingId, bookingId);
        }
    }
}
