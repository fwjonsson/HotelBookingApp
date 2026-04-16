using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Flows.Bookings;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using HotelBookingApp.Utilities.Printers.Customers;

namespace HotelBookingApp.Flows.Customers
{
    internal class CustomerFlows
    {
        private readonly CustomerMenu _customerMenu;

        private readonly CreateCustomerFlow _create;
        private readonly ReadCustomerFlow _read;
        private readonly SelectCustomerFlow _select;

        private readonly CustomerService _customerService;

        internal CustomerFlows(CustomerService customerService)
        {
            _customerMenu = new CustomerMenu();

            _create = new CreateCustomerFlow(customerService);
            _read = new ReadCustomerFlow(customerService);
            _select = new SelectCustomerFlow(customerService);

            _customerService = customerService;
        }

        internal void DisplayBookingsCustomer(int customerId)
        {
            Customer? customer = _customerService.GetById(customerId);
            if (customer is null)
                Console.WriteLine("Customer not found.");
            else
                Console.WriteLine(CustomerPrinter.FormatCustomer(customer));
            InputHelper.WaitForContinue();
        }

        internal void Run()
        {
            while (true)
            {
                var choice = _customerMenu.Show();

                switch (choice)
                {
                    case CustomerMenuOption.Create:
                        _create.Run();
                        break;

                    case CustomerMenuOption.ShowAll:
                        _read.ReadAll();
                        break;

                    case CustomerMenuOption.FindByFullName:
                        _read.ReadByFullName();
                        break;

                    case CustomerMenuOption.FindByBirthDate:
                        _read.ReadByBirthDate();
                        break;

                    case CustomerMenuOption.FindByEmail:
                        _read.ReadByEmail();
                        break;

                    case CustomerMenuOption.FindByPhoneNumber:
                        _read.ReadByPhoneNumber();
                        break;

                    case CustomerMenuOption.FindByID:
                        _read.ReadById();
                        break;

                    case CustomerMenuOption.FindByBookingID:
                        _read.ReadByBookingId();
                        break;

                    case CustomerMenuOption.Back:
                        _customerMenu.ResetMenu();
                        return;
                }
            }
        }
        internal Customer? RunAndGetCustomer()
        {
            Customer? selectedCustomer = null;
            while (true)
            {
                var choice = _customerMenu.Show();

                switch (choice)
                {
                    case CustomerMenuOption.Create:
                        selectedCustomer = _create.Run();
                        break;

                    case CustomerMenuOption.ShowAll:
                        selectedCustomer = _select.ReadAllAndGetSelected();
                        break;

                    case CustomerMenuOption.FindByFullName:
                        selectedCustomer = _select.ReadByFullNameAndGetSelected();
                        break;

                    case CustomerMenuOption.FindByBirthDate:
                        selectedCustomer = _select.ReadByBirthDateAndGetSelected();
                        break;

                    case CustomerMenuOption.FindByEmail:
                        selectedCustomer = _select.ReadByEmailAndGet();
                        break;

                    case CustomerMenuOption.FindByPhoneNumber:
                        selectedCustomer = _select.ReadByPhoneNumberAndGet();
                        break;

                    case CustomerMenuOption.FindByID:
                        selectedCustomer = _select.ReadByIdAndGet();
                        break;

                    case CustomerMenuOption.FindByBookingID:
                        selectedCustomer = _select.ReadByBookingIdAndGet();
                        break;

                    case CustomerMenuOption.Back:
                        _customerMenu.ResetMenu();
                        return null;
                }
                if (selectedCustomer is null)
                    continue;
                _customerMenu.ResetMenu();
                return selectedCustomer;
            }
        }
    }
}
