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
                BirthDate = birthDate,
                Email = email,
                PhoneNumber = phoneNumber
            };
            _dbContext.Customers
                .Add(newCustomer);
            _dbContext.SaveChanges();
            return result;
        }
        internal CustomerValidationResult UpdateCustomer(int customerId, string newFullName, DateOnly newBirthDate, string newEmail, string newPhoneNumber)
        {
            Customer? customer = _dbContext.Customers
                .FirstOrDefault(c => c.Id == customerId);

            var result = new CustomerValidationResult();

            if (customer == null)
                result.CustomerFound = false;
            if (_dbContext.Customers.Any(c => c.PhoneNumber == newPhoneNumber))
                result.PhoneNumberAvailable = false;
        }

        internal CustomerValidationResult DeleteCustomer(int id)
        {
            throw new NotImplementedException();
        }
        internal List<Customer> ListAll()
        {
            return _dbContext.Customers.ToList();
        }
        internal Customer? GetById(int customerId)
        {
            return _dbContext.Customers
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.Id == customerId);
        }
        internal List<Customer> GetByFullName(string fullName)
        {
            return _dbContext.Customers
                .Include(c => c.Bookings)
                .Where(c => c.FullName == fullName)
                .ToList();
        }
        internal List<Customer> GetByBirthDate(DateOnly birthDate)
        {
            return _dbContext.Customers
                .Include(c => c.Bookings)
                .Where(c => c.BirthDate == birthDate)
                .ToList();
        }
        internal Customer? GetByEmail(string email)
        {
            return _dbContext.Customers
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.Email == email);
        }
        internal Customer? GetByPhoneNumber(string phoneNumber)
        {
            return _dbContext.Customers
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }
        internal Customer? GetByBookingId(int bookingId)
        {
            return _dbContext.Customers
                .Include(c=> c.Bookings)
                .FirstOrDefault(c => c.Bookings
                .Any(b => b.Id == bookingId));
        }

        private bool EmailAvailable(string email)
        {
            return !_dbContext.Customers
                .Any(c => c.Email == email);
        }
        private bool PhoneNumberAvailable(string phoneNumber)
        {
            return !_dbContext.Customers
                .Any(c => c.PhoneNumber == phoneNumber);
            //Maybe normalize phone numbers
        }


    }
}
