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
        private readonly SelectObjectMenu _selectCustomerMenu;
        private readonly ActionMenu<Customer> _customerActionMenu;
        private readonly UpdateCustomerFlow _updateCustomerFlow;
        private readonly DeleteCustomerFlow _deleteCustomerFlow;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _readCustomerMenu = new ReadCustomerMenu();
            _selectCustomerMenu = new SelectObjectMenu();
            _customerActionMenu = new ActionMenu<Customer>("Customer",CustomerPrinter.FormatCustomer);
            _updateCustomerFlow = new UpdateCustomerFlow(customerService);
            _deleteCustomerFlow = new DeleteCustomerFlow(customerService);
        }

        internal void Run()
        {
            while (true)
            {
                ReadCustomerMenuOption choice = _readCustomerMenu.Show();

                switch (choice)
                {
                    case ReadCustomerMenuOption.ReadAll:
                        ReadAll();
                        break;
                    case ReadCustomerMenuOption.ReadById:
                        ReadById();
                        break;
                    case ReadCustomerMenuOption.ReadByFullName:
                        ReadByFullName();
                        break;
                    case ReadCustomerMenuOption.ReadByBirthDate:
                        ReadByBirthDate();
                        break;
                    case ReadCustomerMenuOption.ReadByEmail:
                        ReadByEmail();
                        break;
                    case ReadCustomerMenuOption.ReadByPhoneNumber:
                        ReadByPhoneNumber();
                        break;
                    case ReadCustomerMenuOption.ReadByBookingId:
                        ReadByBookingId();
                        break;
                    case ReadCustomerMenuOption.Back:
                        _readCustomerMenu.ResetIndex();
                        return;
                }
                InputHelper.WaitForContinue();
            }
        }
        private void SelectCustomerAndChooseAction(List<Customer> customers)
        {
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
        private void ReadAll()
        {
            List<Customer> allCustomers = _customerService.ListAll();

            if (allCustomers.Count == 0)
            {
                Console.WriteLine("No customers found");
                return;
            }
            SelectCustomerAndChooseAction(allCustomers);
        }
        private void ReadById()
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
        private void ReadByFullName()
        {
            var fullName = InputHelper.ReadFullName("Full name: ");
            List<Customer> customers = _customerService.GetByFullName(fullName);

            foreach (Customer customer in customers)
            {
                CustomerPrinter.PrintCustomerAndTheirBookings(customer);
            }
        }
        private void ReadByBirthDate()
        {
            var birthDate = InputHelper.ReadDateOnly("Birth date: ");
            List<Customer> customers = _customerService.GetByBirthDate(birthDate);

            foreach (var customer in customers)
            {
                CustomerPrinter.PrintCustomerAndTheirBookings(customer);
            }
        }
        private void ReadByEmail()
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
        private void ReadByPhoneNumber()
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
        private void ReadByBookingId()
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
