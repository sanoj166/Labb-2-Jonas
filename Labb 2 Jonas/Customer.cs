using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_Jonas
{
    public class Customer
    {
        public string Name { get; }
        public string Password { get; }

        public List<Product> Cart {  get; } = new List<Product>();

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public bool VerifyPassword(string password)
        {
            return Password == password;
        }
        
        public void AddCart(Product product)
        {
            Cart.Add(product);
        }

        public decimal CalculateCartTotal()
        {
            decimal total = 0;
            foreach (var product in Cart)
            {
                total += product.Price;
            }
            return total;
        }

        public override string ToString()
        {
            return $"Customer: {Name}\nShopping Cart:\n{GetCartContents()}Total Cost: {CalculateCartTotal()}";
        }

        private string GetCartContents()
        {
            string contents = "";
            foreach (var product in Cart)
            {
                contents += $"{product.Name} - {product.Price:C}\n";
            }
            return contents ;
        }
    }
}
