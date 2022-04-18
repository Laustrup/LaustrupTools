using Xunit;

namespace Liszt.tests;

public class LisztTest
{
    
    private Liszt<object> _liszt { get; set; }

    [Theory]
    [InlineData(1)][InlineData(2)][InlineData(3)][InlineData(5)][InlineData(100)][InlineData(1000000)]
    public void AddTest(int amount)
    {
        // Arrange
        DateTime start = DateTime.Now;
        Liszt<object> actOfElement = new Liszt<object>();
        int[] amounts = new int[amount];

        // Act
        for (int i = 0; i < amount; i++)
        {
            actOfElement.Add(i);
            amounts[i] = i;
        }
        
        DateTime end = DateTime.Now;
        Console.Write("Performance time for AddElement = " + end.Subtract(start));

        Liszt<object> actOfElements = new Liszt<object>();
        start = DateTime.Now;
        actOfElements.Add(amounts);
        end = DateTime.Now;
        Console.Write("Performance time for AddElements = " + end.Subtract(start));

        // Assert
        Assert.Equal(amount,actOfElement.Size);
        Assert.Equal(amount,actOfElements.Size);
    }

}