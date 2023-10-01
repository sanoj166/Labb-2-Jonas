using System.ComponentModel.Design;
using System.Net.Http.Headers;

namespace Labb_2_Jonas
{
    public class Program
    {
        private static List<Customer> customers = new List<Customer>()
        {
            new Customer("Knatte", "123"),
            new Customer("Fnatte", "321"),
            new Customer("Tjatte", "213")
        };

        private static List<Product> products = new List<Product>
        {
            new Product("Kielbasa", 10.0M),
            new Product("Cola", 5.0M),
            new Product("Banana", 2.0M)
        };

        private static Customer loggedInCustomer = null;
        public static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Welcome to my simple store!");

                if (loggedInCustomer == null)
                {
                    Console.WriteLine("1. Register a new customer");
                    Console.WriteLine("2. Log in");
                }
                else
                {
                    Console.WriteLine($"Logged in as: {loggedInCustomer.Name}");
                    Console.WriteLine("3. Shop");
                    Console.WriteLine("4. View shopping cart");
                    Console.WriteLine("5. Go to checkout");
                    Console.WriteLine("6. Log out");
                }

                Console.WriteLine("7. Exit");



                

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        RegisterCustomer();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        if (loggedInCustomer != null)
                        {
                            Shop(loggedInCustomer);
                        }
                        else
                        {
                            Console.WriteLine("Please log in first.");
                        }
                        break;
                    case 4:
                        if (loggedInCustomer != null)
                        {
                            Console.WriteLine(loggedInCustomer);  
                        }
                        else
                        {
                            Console.WriteLine("Please log in first.");
                        }
                        break;
                    case 5:
                        if (loggedInCustomer != null)
                        {
                            Checkout(loggedInCustomer);
                        }
                        else
                        {
                            Console.WriteLine("Please log in first.");
                        }
                    break;
                        case 6:
                        if (loggedInCustomer != null)
                        {
                            Logout();
                        }
                        else
                        {
                            
                            Console.WriteLine("Please log in first");
                                
                        }
                    break;
                        case 7:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void Logout()
        {
            loggedInCustomer = null;
            Console.WriteLine("Logged out successfully.");
        }


        private static void RegisterCustomer()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter a password: ");
            string password = Console.ReadLine();

            customers.Add(new Customer(name, password));
            Console.WriteLine("Customer registered!\n");
        }

        private static void Login()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your password: ");
            string password = Console.ReadLine();

            Customer customer = customers.Find(c => c.Name == name);

            if (customer == null)
            {
                Console.WriteLine("Customer does not exist. Do you want to register a new customer? (yes/no)");
                string response = Console.ReadLine().ToLower();
                if (response == "yes")
                {
                    RegisterCustomer();
                }
            }

            else if (!customer.VerifyPassword(password))
            {
                Console.WriteLine("Incorrect password. Please try again. ");
            }
            else
            {
                Shop(customer);
            }

        }

        private static void Shop(Customer customer)
        {
            bool shopping = true;
            while (shopping)
            {
                Console.WriteLine($"Welcome, {customer.Name}!");
                Console.WriteLine("1. Shop");
                Console.WriteLine("2. View shopping cart");
                Console.WriteLine("3. Go to checkout");
                Console.WriteLine("4. Log out");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DisplayProducts();
                        BuyProduct(customer); 
                    break;
                        case 2:
                        Console.WriteLine(customer);
                    break;
                        case 3:
                        Checkout(customer);
                        shopping = false;
                    break;
                        case 4:
                        shopping = false;
                    break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again. ");
                        break;
                }
            }
        }

        private static void DisplayProducts()
        {
            Console.WriteLine("Available products:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price:C}");
            }
        }

        private static void BuyProduct(Customer customer)
        {
            Console.Write("Enter the number of the product you want to buy: ");
            int productIndex = int.Parse(Console.ReadLine()) - 1;

            if (productIndex >= 0 && productIndex < products.Count)
            {
                Product selectedProduct = products[productIndex];

                Console.Write($"Enter the quantity of {selectedProduct.Name} you want to buy: ");
                int quantity = int.Parse(Console.ReadLine());

                if (quantity > 0)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        customer.AddCart(selectedProduct);
                    }

                    Console.WriteLine($"{quantity} {selectedProduct.Name} has been added to your shopping cart.\n");
                }
                else
                {
                    Console.WriteLine("Quantity must be greater than zero. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("invalid choice. PLease try again.");

            }
        }
                
                

        private static void Checkout(Customer customer)
        {
            Console.WriteLine(customer);
            Console.WriteLine("Thank you for your purchase!");
            customer.Cart.Clear();
       
        }

        }
    }

