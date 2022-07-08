using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkEFDapper
{
    public class CustomerService
    {
        private readonly List<Customer> _customers;

        public CustomerService()
        {
            Randomizer.Seed = new Random(10);
            var customerFaker = new Faker<Customer>()
                .RuleFor(x => x.Id, faker => faker.Random.Guid())
                .RuleFor(x => x.FullName, faker => faker.Person.FullName)
                .RuleFor(x => x.Email, faker => faker.Person.Email)
                .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth);

            _customers = customerFaker.Generate(100000);
        }

        public void Print()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine($"Id: {customer.Id} - name: {customer.FullName} - email: {customer.Email} - date of birth: {customer.DateOfBirth}");
            }
        }

        public void InsertEF()
        {
            using (var context = new BenchmarkDbContext())
            {
                context.Customers.AddRange(_customers);
                context.SaveChanges();
            }
        }

        public List<Customer> SelectAllEF()
        {
            using (var context = new BenchmarkDbContext())
            {
                return context.Customers.AsNoTracking().ToList();
            }
        }

        public void DeleteAllEF()
        {
            using (var context = new BenchmarkDbContext())
            {
                var customers = context.Customers.ToList();
                context.RemoveRange(customers);
                context.SaveChanges();
            }
        }

        public void UpdateAllEF()
        {
            using (var context = new BenchmarkDbContext())
            {
                var customers = context.Customers.ToList();

                foreach (var customer in customers)
                {
                    customer.FullName = "teste";
                    customer.Email = "teste@email.com";
                }

                context.UpdateRange(customers);
                context.SaveChanges();
            }
        }
    }
}
