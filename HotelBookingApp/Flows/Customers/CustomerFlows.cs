using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Services.Customers;

namespace HotelBookingApp.Flows.Customers
{
    internal class CustomerFlows
    {
        private readonly CustomerMenu _customerMenu;

        private readonly CreateCustomerFlow _create;
        private readonly ReadCustomerFlow _read;

        internal CustomerFlows(CustomerService customerService)
        {
            _customerMenu = new CustomerMenu();

            _create = new CreateCustomerFlow(customerService);
            _read = new ReadCustomerFlow(customerService);
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
    }
}
