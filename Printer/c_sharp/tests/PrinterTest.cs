using Xunit;
using Xunit.Abstractions;

namespace tests;

// Author Laust Eberhardt Bonnesen
public class PrinterTest : UnitTest
{

    public PrinterTest(ITestOutputHelper output) : base(output) {}
    
    public static IEnumerable<object[]> AddContentInputs()
    {
        yield return new object[] {0};
    }
    [Theory]
    [MemberData(nameof(AddContentInputs))]
    public void PrintTest(string content)
    {
        // Act
        DetermineStart();
        
        DetermineEnd();
        CalculatePerformanceTime();

        // Assert
    }
    
    public static IEnumerable<object[]> AddArrayInputs()
    {
        yield return new object[] {0};
    }
    [Theory]
    [MemberData(nameof(AddArrayInputs))]
    public void ArrayTest(object[] array)
    {
        // Act
        DetermineStart();
        
        DetermineEnd();
        CalculatePerformanceTime();
        
        // Assert
        
    }
    
    
}