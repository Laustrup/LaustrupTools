using Xunit;
using Xunit.Abstractions;

namespace Liszt.tests;

public class LisztTest
{
    
    private Liszt<object> _liszt { get; set; }
    
    private DateTime _start { get; set; }
    private DateTime _end { get; set; }
    
    private readonly ITestOutputHelper _output;

    public LisztTest(ITestOutputHelper output) {_output=output;}
    
    
    public static IEnumerable<object[]> AddInputs()
    {
        yield return new object[] {0};
        yield return new object[] {1};
        yield return new object[] {2};
        yield return new object[] {3};
        yield return new object[] {4};
        yield return new object[] {5};
        yield return new object[] {10};
        yield return new object[] {100};
        yield return new object[] {1000};
    }
    [Theory]
    [MemberData(nameof(AddInputs))]
    public void AddTest(int amount)
    {
        // Arrange
        _start = DateTime.Now; 
        _liszt = new Liszt<object>();
        int[] amountAsArray = new int[amount];

        // Act
        for (int i = 0; i < amount; i++)
        {
            _liszt.Add(i);
            amountAsArray[i] = i;
        }
        
        _end = DateTime.Now;
        _output.WriteLine("Performance time for AddElement = " + _end.Subtract(_start));
        
        AssertAdd(amount);
        
        _liszt.ClearAll();
            
        _start = DateTime.Now;
        _liszt.Add(amountAsArray);
        _end = DateTime.Now;
        
        if(amount!=0) {AssertAdd(amount);}
        _output.WriteLine("Performance time for AddElements = " + _end.Subtract(_start));
            
        _output.WriteLine("\nVersion of Liszt = " + Liszt<object>.Version);
        
    }
    private void AssertAdd(int amount) { Assert.Equal(amount,_liszt.Size); }


}