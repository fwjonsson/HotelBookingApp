using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.FlowHelpers;
using HotelBookingApp.Utilities.Printers.Bookings;
using HotelBookingApp.Utilities.Printers.Customers;

namespace HotelBookingApp.Flows.Customers
{
    internal class ReadCustomerFlow
    {
        private readonly CustomerService _customerService;
        private readonly SelectObjectMenu<Customer> _selectCustomerMenu;
        private readonly CustomerActionMenu<Customer> _customerActionMenu;
        private readonly UpdateCustomerFlow _updateCustomerFlow;
        private readonly DeleteCustomerFlow _deleteCustomerFlow;


        internal ReadCustomerFlow(CustomerService customerService)
        {
            _customerService = customerService;
            _selectCustomerMenu = new SelectObjectMenu<Customer>();
            _customerActionMenu = new CustomerActionMenu<Customer>("--Customer--", CustomerPrinter.FormatCustomer);
            _updateCustomerFlow = new UpdateCustomerFlow(customerService);
            _deleteCustomerFlow = new DeleteCustomerFlow(customerService);
        }

        private void SelectCustomerAndChooseAction(Func<List<Customer>> getCustomers, Func<Customer, string> formatter, string title)
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

                Customer? customer = _selectCustomerMenu.Show(customers, formatter, title);
                if (customer == null)
                {
                    _selectCustomerMenu.ResetMenu();
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

                CustomerActionMenuOption choice = _customerActionMenu.Show(customer!);

                switch (choice)
                {
                    case CustomerActionMenuOption.ShowBookings:
                        ReadAllBookingsByCustomer(customer!.Id);
                        break;

                    case CustomerActionMenuOption.Edit:
                        _updateCustomerFlow.Run(customer!.Id);
                        break;

                    case CustomerActionMenuOption.Delete:
                        if (_deleteCustomerFlow.TryDelete(customer!.Id))
                        {
                            _customerActionMenu.ResetMenu();
                            return;
                        }
                        break;

                    case CustomerActionMenuOption.Back:
                        _customerActionMenu.ResetMenu();
                        return;
                }
            }
        }
        internal void ReadAll()
        {
            SelectCustomerAndChooseAction(_customerService.ListAll, CustomerPrinter.MenuShowAllFormatCustomer,
                "--All Customers--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |     Phone Nr  \n" + new string('-', 103));
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
            SelectCustomerAndChooseAction(() => _customerService.GetByFullName(fullName), CustomerPrinter.MenuShowAllFormatCustomer,
                $"--Customers By Name: {fullName}--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |     Phone Nr  \n" + new string('-', 103));
        }

        internal void ReadByBirthDate()
        {
            DateOnly? birthDateInput = InputHelper.ReadDateOnly("Birth Date: ");
            if (birthDateInput is not DateOnly birthDate)
                return;
            SelectCustomerAndChooseAction(() => _customerService.GetByBirthDate(birthDate), CustomerPrinter.MenuShowAllFormatCustomer,
                $"--Customers With Birth Date: {birthDate:yyyy-MM-dd}--\n" +
                "     ID |           Full Name           | Birth Date |             Email             |     Phone Nr  \n" + new string('-', 103));
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
        internal void ReadAllBookingsByCustomer(int customerId)
        {
            List<Booking> bookingsByCustomer = _customerService.GetAllBookingsByCustomer(customerId);
            if (bookingsByCustomer.Count == 0)
            {
                Console.WriteLine("No bookings found.");
                InputHelper.WaitForContinue();
                return;
            }
            Console.Clear();
            foreach (Booking booking in bookingsByCustomer)
            {
                Console.WriteLine(BookingPrinter.FormatBooking(booking));
            }
            InputHelper.WaitForContinue();
        }
    }
}
