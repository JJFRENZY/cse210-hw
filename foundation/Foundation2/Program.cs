using System;
using System.Collections.Generic;

namespace ProductOrderingSystem
{
    // Address Class
    public class Address
    {
        private string street;
        private string city;
        private string stateOrProvince;
        private string country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            this.street = street;
            this.city = city;
            this.stateOrProvince = stateOrProvince;
            this.country = country;
        }

        public bool IsInUSA()
        {
            return country.ToLower() == "usa";
        }

        public override string ToString()
        {
            return $"{street}\n{city}, {stateOrProvince}\n{country}";
        }
    }

    // Customer Class
    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool LivesInUSA()
        {
            return address.IsInUSA();
        }

        public string GetName()
        {
            return name;
        }

        public Address GetAddress()
        {
            return address;
        }
    }

    // Product Class
    public class Product
    {
        private string name;
        private string productId;
        private decimal price;
        private int quantity;

        public Product(string name, string productId, decimal price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        public decimal GetTotalCost()
        {
            return price * quantity;
        }

        public string GetPackingInfo()
        {
            return $"{name} (ID: {productId})";
        }
    }

    // Order Class
    public class Order
    {
        private List<Product> products;
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
            this.products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public decimal GetTotalPrice()
        {
            decimal total = 0;
            foreach (var product in products)
            {
                total += product.GetTotalCost();
            }

            // Add shipping cost
            total += customer.LivesInUSA() ? 5 : 35;
            return total;
        }

        public string GetPackingLabel()
        {
            string packingLabel = "Packing Label:\n";
            foreach (var product in products)
            {
                packingLabel += $"- {product.GetPackingInfo()}\n";
            }
            return packingLabel;
        }

        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Create customers and their addresses
            var address1 = new Address("123 Main St", "Springfield", "IL", "USA");
            var customer1 = new Customer("John Doe", address1);

            var address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");
            var customer2 = new Customer("Jane Smith", address2);

            // Create orders
            var order1 = new Order(customer1);
            order1.AddProduct(new Product("Laptop", "LAP123", 800.00m, 1));
            order1.AddProduct(new Product("Mouse", "MSE456", 20.00m, 2));

            var order2 = new Order(customer2);
            order2.AddProduct(new Product("Phone", "PHN789", 600.00m, 1));
            order2.AddProduct(new Product("Charger", "CHR101", 15.00m, 2));

            // Display details for Order 1
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order1.GetTotalPrice()}");
            Console.WriteLine(new string('-', 40));

            // Display details for Order 2
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order2.GetTotalPrice()}");
        }
    }
}
