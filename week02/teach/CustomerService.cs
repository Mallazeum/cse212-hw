/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Specifying a queue that is to small will result in the queue being defaulted to size 10 and create one of a specified size
        // Expected Result: There will be 10 customers slots when input is 0 or less the specified size is used for the specified queue
        Console.WriteLine("Test 1");
        var cs = new CustomerService(0);
        var sq = new CustomerService(1);
        Console.WriteLine(cs);
        Console.WriteLine(sq);

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 2
        // Scenario: adding customers to the queue
        // Expected Result: The customers are added as a queue
        Console.WriteLine("Test 2");
        cs.AddNewCustomer();
        Console.WriteLine();
        cs.AddNewCustomer();
        Console.WriteLine();
        // Console.WriteLine(cs);

        // Defect(s) Found: none

        Console.WriteLine("=================");

        // Test 3
        // Scenario: removing customers
        // Expected Result: the customers are removed fifo
        Console.WriteLine("Test 3");
        cs.ServeCustomer();
        Console.WriteLine();
        Console.WriteLine(cs);

        // Defect(s) Found: It would remove the first item before displaying the first item so 
        //so that the item shown is the new first not the one being served

        Console.WriteLine("=================");

        // Test 4 
        // Scenario: removing a customer from an empty queue
        // Expected Result: Error message displayed
        Console.WriteLine("Test 4");
        cs.ServeCustomer(); // Remove the second one from test 2 that wasn't removed in test 3 
        cs.ServeCustomer(); // This one should display the error

        // Defect(s) Found: Serve customer was not checking if the queue was empty and there was no code to display any error message

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Adding a customer to a full queue
        // Expected Result: Error message displayed
        Console.WriteLine("Test 5");
        // Uses sq which was set to have a max size of one in test 1
        sq.AddNewCustomer();
        sq.AddNewCustomer();// This one should throw the error

        // Defect(s) Found: When checking if the max size had been reached in the queue it was checking if the count of the queue was greater than max count allowing one more thaan the max into the queue

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count == _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if(_queue.Count <= 0)
        {
            Console.WriteLine("ERROR: There are no more customers to serve");
        }
        else
        {
            var customer = _queue[0];
            _queue.RemoveAt(0);
            Console.WriteLine(customer);
        }
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}