using Microsoft.EntityFrameworkCore;
using WebSwagger.Models;

namespace WebSwagger.Data
{
    public class CustomerRepository : DbContext
    {
        public CustomerRepository(DbContextOptions<CustomerRepository> options) : base(options)
        {

        }

        public DbSet<Customer> Customers => Set<Customer>();
    }
}
