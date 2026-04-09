using HotelBookingApp.Data;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Services.Customers
{
    internal class CustomerService
    {
        private readonly ApplicationDbContext _dbContext;

        internal CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal CustomerValidationResult CreateCustomer(string fullName, DateOnly birthDate, string email, string phoneNumber)
        {
            var result = new CustomerValidationResult();
            if (!EmailAvailable(email))
            {
                result.EmailAvailable = false;
            }
            if (!PhoneNumberAvailable(phoneNumber))
            {
                result.PhoneNumberAvailable = false;
            }
            if (!result.Success)
            {
                return result;
            }

            var newCustomer = new Customer
            {
                FullName = fullName,
                Birthdate = birthDate,
                Email = email,
                PhoneNumber = phoneNumber
            };
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
            return result;
        }
        private bool EmailAvailable(string email)
        {
            return !_dbContext.Customers.Any(c => c.Email == email);
        }
        private bool PhoneNumberAvailable(string phoneNumber)
        {
            return !_dbContext.Customers.Any(c => c.PhoneNumber == phoneNumber);
            //Maybe normalize phone numbers
        }
    }
}
