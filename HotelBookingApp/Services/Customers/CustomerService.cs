using HotelBookingApp.Data;
using HotelBookingApp.Models;
using Microsoft.EntityFrameworkCore;

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
                result.EmailAvailable = false;

            if (!PhoneNumberAvailable(phoneNumber))
                result.PhoneNumberAvailable = false;

            if (!IsAdult(birthDate))
                result.IsAdult = false;

            if (!result.Success)
                return result;

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
            {
                result.CustomerFound = false;
                return result;
            }
            if (!EmailAvailable(newEmail, customerId))
                result.EmailAvailable = false;

            if (!PhoneNumberAvailable(newPhoneNumber, customerId))
                result.PhoneNumberAvailable = false;

            if (!IsAdult(newBirthDate))
                result.IsAdult = false;

            if (!result.Success)
                return result;

            customer!.FullName = newFullName;
            customer.BirthDate = newBirthDate;
            customer.Email = newEmail;
            customer.PhoneNumber = newPhoneNumber;

            _dbContext.SaveChanges();
            return result;
        }

        internal CustomerValidationResult DeleteCustomer(int customerId)
        {
            Customer? customer = _dbContext.Customers
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.Id == customerId);

            var result = new CustomerValidationResult();

            if (customer == null)
            {
                result.CustomerFound = false;
                return result;
            }

            if (HasCurrentOrFutureBooking(customerId))
                result.CanBeDeleted = false;

            if (!result.Success)
                return result;

            _dbContext.Customers
                .Remove(customer);
            _dbContext.SaveChanges();
            return result;
        }
        internal List<Customer> ListAll()
        {
            return _dbContext.Customers
                .ToList();
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
                .Include(c => c.Bookings)
                .FirstOrDefault(c => c.Bookings
                .Any(b => b.Id == bookingId));
        }
        private bool IsAdult(DateOnly birthDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return birthDate.AddYears(18) <= today;
        }

        private bool EmailAvailable(string email, int? customerIdToExclude = null)
        {
            return !_dbContext.Customers
                .Any(c =>
                (customerIdToExclude == null || c.Id != customerIdToExclude) &&
                c.Email == email);
        }
        private bool PhoneNumberAvailable(string phoneNumber, int? customerIdToExclude = null)
        {
            return !_dbContext.Customers
                .Any(c =>
                (customerIdToExclude == null || c.Id != customerIdToExclude) &&
                c.PhoneNumber == phoneNumber);
            //Maybe normalize phone numbers
        }

        private bool HasCurrentOrFutureBooking(int customerId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return _dbContext.Bookings
                .Any(b => b.CustomerId == customerId &&
                b.EndDate > today);
        }
    }
}
