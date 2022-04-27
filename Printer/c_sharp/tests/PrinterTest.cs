using c_sharp;
using Xunit;
using Xunit.Abstractions;

namespace tests;

// Author Laust Eberhardt Bonnesen
public class PrinterTest : UnitTest
{

    public PrinterTest(ITestOutputHelper output) : base(output) {}
    
    public static IEnumerable<object[]> AddContentInputs()
    {
        yield return new object[] {"Hello world","\nHello world\n\n"};
        yield return new object[] {"1","\n1\n\n"};
        yield return new object[] {"1.00","\n1.00\n\n"};
        yield return new object[] {"false","\nfalse\n\n"};
        yield return new object[] {"null","\nnull\n\n"};
        yield return new object[] {"\n","\n\n\n\n"};
    }
    [Theory]
    [MemberData(nameof(AddContentInputs))]
    public void PrintTest(string content,string expected)
    {
        // Act
        Start();
        // Assert
        Assert.Equal(expected,Printer.Print(content));
        _output.WriteLine(Printer.LastPrint);
        End();
        
    }
    
    public static IEnumerable<object[]> AddArrayInputs()
    {
        yield return new object[] {new []{1,2,3},"{1 - 2 - 3}"};
        yield return new object[] {new []{"Hello","World"},"{Hello - World}"};
        yield return new object[] {new []{true,false},"{true - false}"};
    }
    [Theory]
    [MemberData(nameof(AddArrayInputs))]
    public void ArrayTest(object[] array,string expected)
    {
        // Act
        Start();
        // Assert
        Assert.Equal(expected,Printer.Print(array));
        _output.WriteLine(Printer.LastPrint);
        End();
    }
    
    
}