using Xunit;
using Xunit.Abstractions;

namespace Liszt.tests;

// Author Laust Eberhardt Bonnesen
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
        _liszt = new Liszt<object>();
        
        int[] expected = new int[amount];
        for (int i = 0; i < amount; i++) { expected[i] = i+1; }

        _output.WriteLine(expected.Length.ToString());
        
        // Act
        _start = DateTime.Now;
        _liszt.Add(expected);
        _end = DateTime.Now;
        _output.WriteLine("Performance time = " + _end.Subtract(_start));
        
        // Assert
        Assert.Equal(amount,_liszt.Size);
        _liszt.ClearAll();
    }
}