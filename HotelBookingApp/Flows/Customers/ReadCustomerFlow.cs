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
            _customerActionMenu = new ActionMenu<Customer>("--Customer--", CustomerPrinter.MenuFormatCustomer);
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
                    Console.WriteLine("No customers found");
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
            var customerId = InputHelper.ReadInt("Customer ID: ");
            ChooseAction(_customerService.GetById, customerId);
        }
        internal void ReadByFullName()
        {
            var fullName = InputHelper.ReadFullName("Full Name: ");
            SelectCustomerAndChooseAction(() => _customerService.GetByFullName(fullName), $"--Customers By Name: {fullName}--");
        }
        internal void ReadByBirthDate()
        {
            var birthDate = InputHelper.ReadDateOnly("Birth Date: ");
            SelectCustomerAndChooseAction(() => _customerService.GetByBirthDate(birthDate), $"--Customers With Birth Date: {birthDate:yyyy-MM-dd}--");
        }
        internal void ReadByEmail()
        {
            var email = InputHelper.ReadEmail("Email: ");
            ChooseAction(_customerService.GetByEmail, email);
        }
        internal void ReadByPhoneNumber()
        {
            var phoneNumber = InputHelper.ReadPhoneNumber("Phone Number: ");
            ChooseAction(_customerService.GetByPhoneNumber, phoneNumber);
        }
        internal void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Booking ID: ");
            ChooseAction(_customerService.GetByBookingId, bookingId);
        }
    }
}
