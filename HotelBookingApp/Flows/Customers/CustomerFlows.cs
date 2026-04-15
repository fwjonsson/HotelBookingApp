using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Models;
using HotelBookingApp.Services.Customers;

namespace HotelBookingApp.Flows.Customers
{
    internal class CustomerFlows
    {
        private readonly CustomerMenu _customerMenu;

        private readonly CreateCustomerFlow _create;
        private readonly ReadCustomerFlow _read;
        private readonly SelectCustomerFlow _select;

        internal CustomerFlows(CustomerService customerService)
        {
            _customerMenu = new CustomerMenu();

            _create = new CreateCustomerFlow(customerService);
            _read = new ReadCustomerFlow(customerService);
            _select = new SelectCustomerFlow(customerService);
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
                        _customerMenu.ResetIndex();
                        return;
                }
            }
        }
        internal Customer? RunAndGetCustomer()
        {
            while (true)
            {
                var choice = _customerMenu.Show();

                switch (choice)
                {
                    case CustomerMenuOption.Create:
                        return _create.Run();

                    case CustomerMenuOption.ShowAll:
                        return _select.ReadAllAndGetSelected();

                    case CustomerMenuOption.FindByFullName:
                        return _select.ReadByFullNameAndGetSelected();

                    case CustomerMenuOption.FindByBirthDate:
                        return _select.ReadByBirthDateAndGetSelected();

                    case CustomerMenuOption.FindByEmail:
                        return _select.ReadByEmailAndGet();

                    case CustomerMenuOption.FindByPhoneNumber:
                        return _select.ReadByPhoneNumberAndGet();

                    case CustomerMenuOption.FindByID:
                        return _select.ReadByIdAndGet();
                        
                    case CustomerMenuOption.FindByBookingID:
                        return _select.ReadByBookingIdAndGet();

                    case CustomerMenuOption.Back:
                        _customerMenu.ResetIndex();
                        return null;
                }
            }
        }
    }
}
