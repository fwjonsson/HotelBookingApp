using HotelBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services
{
    internal class CustomerService
    {
        private readonly ApplicationDbContext _dbContext;

        internal CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
