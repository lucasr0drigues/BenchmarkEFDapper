using Bogus;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BenchmarkEFDapper
{
    public class CustomerService
    {
        private List<Customer> _customers;
        private readonly string connectionString = "Server=localhost,1433;Database=BenchmarkDapperEF;User ID=sa;Password=1q2w3e4r@#$";

        public CustomerService()
        {
        }

        public void Print()
        {
            foreach (var customer in _customers)
            {
                Console.WriteLine($"Id: {customer.Id} - name: {customer.FullName} - email: {customer.Email} - date of birth: {customer.DateOfBirth}");
            }
        }

        public void InsertEF(int regs)
        {
            GenerateCustomers(regs);

            using (var context = new BenchmarkDbContext())
            {
                context.Customers.AddRange(_customers);
                context.SaveChanges();
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

        public void InsertDapperContrib(int regs)
        {
            GenerateCustomers(regs);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                connection.Insert<List<Customer>>(_customers, trans);
                trans.Commit();
            }
        }

        public void DeleteDapperContrib()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.DeleteAll<Customer>();
            }
        }

        public void GenerateCustomers(int regs)
        {
            Randomizer.Seed = new Random(10);
            var customerFaker = new Faker<Customer>()
                .RuleFor(x => x.Id, faker => faker.Random.Guid())
                .RuleFor(x => x.FullName, faker => faker.Person.FullName)
                .RuleFor(x => x.Email, faker => faker.Person.Email)
                .RuleFor(x => x.DateOfBirth, faker => faker.Person.DateOfBirth);

            _customers = customerFaker.Generate(regs);
        }


    }
}
