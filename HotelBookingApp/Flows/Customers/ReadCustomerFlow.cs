using HotelBookingApp.Enums.Menus;
using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;

namespace HotelBookingApp.Flows.Customers
{
    internal class ReadCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly ReadCustomerMenu _readCustomerMenu;
        private readonly SelectObjectMenu<Customer> _selectCustomerMenu;
        private readonly ActionMenu<Customer> _customerActionMenu;
        private readonly UpdateCustomerFlow _updateCustomerFlow;
        private readonly DeleteCustomerFlow _deleteCustomerFlow;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _readCustomerMenu = new ReadCustomerMenu();
            _selectCustomerMenu = new SelectObjectMenu<Customer>("Customer", CustomerPrinter.FormatCustomer);
            _customerActionMenu = new ActionMenu<Customer>("Customer",CustomerPrinter.FormatCustomer);
            _updateCustomerFlow = new UpdateCustomerFlow(customerService);
            _deleteCustomerFlow = new DeleteCustomerFlow(customerService);
        }

        private void SelectCustomerAndChooseAction(List<Customer> customers)
        {
            while (true)
            {
                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found");
                    return;
                }
                Customer? selectedCustomer = _selectCustomerMenu.Show(customers);
            if (selectedCustomer == null)
                return;

            var choice = _customerActionMenu.Show(selectedCustomer);

                switch (choice)
                {
                    case ActionMenuOption.Edit:
                        _updateCustomerFlow.Run(selectedCustomer);
                        break;
                    case ActionMenuOption.Delete:
                        _deleteCustomerFlow.Run(selectedCustomer);
                        break;
                    case ActionMenuOption.Back:
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            List<Customer> allCustomers = _customerService.ListAll();

            SelectCustomerAndChooseAction(allCustomers);
        }
        internal void ReadById()
        {
            var customerId = InputHelper.ReadInt("Enter customer ID: ");
            Customer? customer = _customerService.GetById(customerId);

            if (customer == null)
            {
                Console.WriteLine("No customer with that ID found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer);
        }
        internal void ReadByFullName()
        {
            var fullName = InputHelper.ReadFullName("Full name: ");
            List<Customer> customers = _customerService.GetByFullName(fullName);

            foreach (Customer customer in customers)
            {
                CustomerPrinter.PrintCustomerAndTheirBookings(customer);
            }
        }
        internal void ReadByBirthDate()
        {
            var birthDate = InputHelper.ReadDateOnly("Birth date: ");
            List<Customer> customers = _customerService.GetByBirthDate(birthDate);

            foreach (var customer in customers)
            {
                CustomerPrinter.PrintCustomerAndTheirBookings(customer);
            }
        }
        internal void ReadByEmail()
        {
            var email = InputHelper.ReadEmail("Email: ");
            Customer? customer = _customerService.GetByEmail(email);

            if (customer == null)
            {
                Console.WriteLine("No customer with that email found.");
                return;
            }

            CustomerPrinter.PrintCustomerAndTheirBookings(customer);
        }
        internal void ReadByPhoneNumber()
        {
            var phoneNumber = InputHelper.ReadPhoneNumber("Phone number: ");
            Customer? customer = _customerService.GetByPhoneNumber(phoneNumber);

            if (customer == null)
            {
                Console.WriteLine("No customer with that phone number found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer);
        }
        internal void ReadByBookingId()
        {
            var bookingId = InputHelper.ReadInt("Booking ID: ");
            Customer? customer = _customerService.GetByBookingId(bookingId);

            if (customer == null)
            {
                Console.WriteLine("No customer with that booking ID found.");
                return;
            }
            CustomerPrinter.PrintCustomerAndTheirBookings(customer);
        }
    }
}
