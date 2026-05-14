using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Creates a queue with the following values James(3), Steve(2), and bob(1) then dequeues until the queue is empty
    // Expected Result: Bob, steve, James
    // Defect(s) Found: The dequeue function did not remove the item from the queue added a line of code to properly
    // testing if items enqueued and dequeued in fifo order
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        var james = new PriorityItem("James",3);
        var steve = new PriorityItem("Steve",2);
        var bob = new PriorityItem("Bob",1);

        priorityQueue.Enqueue(james.Value, james.Priority);
        priorityQueue.Enqueue(steve.Value, steve.Priority);
        priorityQueue.Enqueue(bob.Value, bob.Priority);


        PriorityItem[] expectedResult = [james, steve, bob];
        
        foreach(var item in expectedResult)
        {
            string person = priorityQueue.Dequeue();
            Assert.AreEqual(item.Value,person);
        }
    }

    [TestMethod]
    // Scenario: Create a queue with the following values James(1), Steve(2), Bob(3) and then dequeue until empty
    // Expected Result:Bob, Steve, James
    // Defect(s) Found: When looping through each item the last item was not being checked against the others
    // This was fixed by changing i < count-1 to i < count
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        var james = new PriorityItem("James",1);
        var steve = new PriorityItem("Steve",2);
        var bob = new PriorityItem("Bob",3);

        priorityQueue.Enqueue(james.Value, james.Priority);
        priorityQueue.Enqueue(steve.Value, steve.Priority);
        priorityQueue.Enqueue(bob.Value, bob.Priority);


        PriorityItem[] expectedResult = [bob, steve, james];
        
        foreach(var item in expectedResult)
        {
            string person = priorityQueue.Dequeue();
            Assert.AreEqual(item.Value,person);
        }
    }
    
    [TestMethod]
    // Scenario: Create a queue with the following values James(3), Steve(1), Bob(3), Andy(2) Dequeue until empty
    // Expected Result: James, Bob, Andy, Steve
    // Defect(s) Found: When checking which value had the highest priority they were checking which ones were greater than or equal to leading to the last item in the queue with high priority to be dequeued first
    // Changed it to be checking only greater than so that the first item with the highest priority is dequeued first
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        var james = new PriorityItem("James",3);
        var steve = new PriorityItem("Steve",1);
        var bob = new PriorityItem("Bob",3);
        var andy = new PriorityItem("Andy", 2);

        priorityQueue.Enqueue(james.Value, james.Priority);
        priorityQueue.Enqueue(steve.Value, steve.Priority);
        priorityQueue.Enqueue(bob.Value, bob.Priority);
        priorityQueue.Enqueue(andy.Value, andy.Priority);


        PriorityItem[] expectedResult = [james, bob, andy, steve];
        
        foreach(var item in expectedResult)
        {
            string person = priorityQueue.Dequeue();
            Assert.AreEqual(item.Value,person);
        }
    }

    [TestMethod]
    // Scenario: Try to get the next item from an empty queue
    // Expected Result: InvalidOperationException with a message of "The queue is empty." 
    // Defect(s) Found: None
    // Test if the proper error message is thrown when dequeue an empty queue
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message) );
        }
    }
    // Add more test cases as needed below.
}