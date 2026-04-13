using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers;

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
            _selectCustomerMenu = new SelectObjectMenu<Customer>("Customer", CustomerPrinter.FormatCustomer);
            _customerActionMenu = new ActionMenu<Customer>("Customer", CustomerPrinter.FormatCustomer);
            _updateCustomerFlow = new UpdateCustomerFlow(customerService);
            _deleteCustomerFlow = new DeleteCustomerFlow(customerService);
        }

        private void SelectCustomerAndChooseAction(Func<List<Customer>> getCustomers)
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
                Customer? customer = _selectCustomerMenu.Show(customers);
                if (customer == null)
                {
                    _selectCustomerMenu.ResetIndex();
                    return;
                }


            }
        }
        private void ChooseAction<TId>(Func<TId, Customer?> getCustomer, TId identifier)
        {
            while (true)
            {
                Customer? customer = getCustomer(identifier);
                FlowHelper.EnsureFound(customer, "Customer not found.");
                if (customer == null)
                    return;

                ActionMenuOption choice = _customerActionMenu.Show(customer);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateCustomerFlow.Run(customer.Id);
                        break;
                    case ActionMenuOption.Delete:
                        _deleteCustomerFlow.Run(customer.Id);
                        _customerActionMenu.ResetIndex();
                        return;
                    case ActionMenuOption.Back:
                        _customerActionMenu.ResetIndex();
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            SelectCustomerAndChooseAction(_customerService.ListAll);
        }
        internal void ReadById()
        {
            var customerId = InputHelper.ReadInt("Enter customer ID: ");
            ChooseAction(_customerService.GetById, customerId);
        }
        internal void ReadByFullName()
        {
            var fullName = InputHelper.ReadFullName("Full name: ");
            SelectCustomerAndChooseAction(() => _customerService.GetByFullName(fullName));
        }
        internal void ReadByBirthDate()
        {
            var birthDate = InputHelper.ReadDateOnly("Birth date: ");
            SelectCustomerAndChooseAction(() => _customerService.GetByBirthDate(birthDate));
        }
        internal void ReadByEmail()
        {
            var email = InputHelper.ReadEmail("Email: ");
            ChooseAction(_customerService.GetByEmail,email);
        }
        internal void ReadByPhoneNumber()
        {
            var phoneNumber = InputHelper.ReadPhoneNumber("Phone number: ");
            ChooseAction(_customerService.GetByPhoneNumber, phoneNumber);
        }
        internal void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Booking ID: ");
            ChooseAction(_customerService.GetByBookingId, bookingId);
        }
    }
}
