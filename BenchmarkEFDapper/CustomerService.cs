using Bogus;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public void InsertEFOneByOne(int regs)
        {
            GenerateCustomers(regs);

            using (var context = new BenchmarkDbContext())
            {
                foreach (var customer in _customers)
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }
        }

        public void UpdateAllEF()
        {
            using (var context = new BenchmarkDbContext())
            {
                var customers = context.Customers.ToList();
                var rnd = new Random();

                foreach (var customer in customers)
                {
                    customer.FullName = "teste" + rnd.Next(100);
                    customer.Email = "teste@email.com";
                }

                context.UpdateRange(customers);
                context.SaveChanges();
            }
        }

        public void UpdateAllEFOneByOne()
        {
            using (var context = new BenchmarkDbContext())
            {
                var customers = context.Customers.ToList();
                var rnd = new Random();

                foreach (var customer in customers)
                {
                    customer.FullName = "teste" + rnd.Next(100);
                    customer.Email = "teste@email.com";
                    context.UpdateRange(customer);
                    context.SaveChanges();
                }
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
                connection.Insert<List<Customer>>(_customers);
            }
        }

        public void InsertDapperContribOneByOne(int regs)
        {
            GenerateCustomers(regs);

            using (var connection = new SqlConnection(connectionString))
            {
                foreach (var customer in _customers)
                {
                    connection.Insert(customer);
                }
            }
        }

        public void UpdateDapperContrib()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customers = connection.GetAll<Customer>();
                var rnd = new Random();

                foreach (var customer in customers)
                {
                    customer.FullName = "teste" + rnd.Next(100);
                    customer.Email = "teste@email.com";
                }

                connection.Update(customers);
            }
        }

        public void UpdateDapperContribOneByOne()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                var customers = connection.GetAll<Customer>();
                var rnd = new Random();

                foreach (var customer in customers)
                {
                    customer.FullName = "teste" + rnd.Next(100);
                    customer.Email = "teste@email.com";
                    connection.Update(customer);
                }
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
