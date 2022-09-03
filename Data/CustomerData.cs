using WebSwagger.Models;

namespace WebSwagger.Data
{
    public class CustomerData
    {
        private List<Customer> _Customers = new List<Customer> {
            new Customer(){ Id=1, name= "Abe", password ="123"},
            new Customer(){ Id=2, name= "Bram",password = "123"},
            new Customer(){ Id=3, name= "Cici",password = "123"},
            new Customer(){ Id=4, name= "Dede",password = "123}"}
        };

        public List<Customer> Customers { get { return _Customers; } }
        public Customer? GetCustomer(string name, string password)
        {
            var user = this._Customers.AsEnumerable()
                .Where(c => c.name.Equals(name))
                .FirstOrDefault();

            if (user is null) return null;

            return user;
        }

        public void AddCustomer(Customer customer)
        {
            if (this._Customers.Contains(customer)) return;

            this._Customers.Add(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            if (!this._Customers.Contains(customer)) return;

            this._Customers.Remove(customer);
        }

        public bool ValidatePassword(string name, string password)
        {
            var user = this._Customers.AsEnumerable()
                .Where(c => c.name.Equals(name) && c.password == password)
                .DefaultIfEmpty(null)
                .FirstOrDefault();

            if (user is null) return false;

            return true;
        }
    }
}
