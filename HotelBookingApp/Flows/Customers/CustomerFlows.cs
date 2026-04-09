using HotelBookingApp.Enums.Menus.Customers;
using HotelBookingApp.Menus.Customers;
using HotelBookingApp.Services.Customers;
using HotelBookingApp.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Flows.Customers
{
    internal class CustomerFlows
    {
        private readonly CustomerMenu _customerMenu;

        private readonly CreateCustomerFlow _create;
        private readonly ReadCustomerFlow _read;
        private readonly UpdateCustomerFlow _update;
        private readonly DeleteCustomerFlow _delete;

        internal CustomerFlows(CustomerService customerService,CustomerMenu customerMenu)
        {
            _customerMenu = customerMenu;

            _create = new CreateCustomerFlow(customerService);
            _read = new ReadCustomerFlow(customerService);
            _update = new UpdateCustomerFlow(customerService);
            _delete = new DeleteCustomerFlow(customerService);
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
                    case CustomerMenuOption.Read:
                        _read.Run();
                        break;
                    case CustomerMenuOption.Update:
                        _update.Run();
                        break;
                    case CustomerMenuOption.Delete:
                        _delete.Run();
                        break;
                    case CustomerMenuOption.Back:
                        _customerMenu.ResetIndex();
                        return;
                }
            }
        }
    }
}
