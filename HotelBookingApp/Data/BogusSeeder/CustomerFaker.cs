using Bogus;
using HotelBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBookingApp.Data.BogusSeeder
{
    internal sealed class CustomerFaker : Faker<Customer>
    {
        internal CustomerFaker()
        {
            RuleFor(c => c.FullName, f => f.Name.FullName());

            RuleFor(c => c.BirthDate, f =>
            DateOnly.FromDateTime(f.Date.Past(60, DateTime.Today.AddYears(18))));

            RuleFor(c => c.Email, f =>
            $"guest{f.UniqueIndex}@example.com");

            RuleFor(c => c.PhoneNumber, f =>
            $"+4670{f.UniqueIndex:0000000}");
        }
    }
}
