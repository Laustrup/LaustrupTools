using tools;
using Xunit.Abstractions;

namespace tests;

public abstract class UnitTest
{
    protected DateTime _start { get; set; }
    protected DateTime _end { get; set; }
    
    protected readonly ITestOutputHelper _output;
    
    public UnitTest(ITestOutputHelper output) {_output = output;}

    protected TimeSpan CalculatePerformanceTime()
    {
        TimeSpan performanceTime = _end.Subtract(_start);
        Printer.Print("\tPerformance time = " + performanceTime.TotalMinutes);
        _output.WriteLine(Printer.LastPrint);
        return performanceTime;
    }

    protected DateTime DetermineStart() { return _start = DateTime.Now; }
    protected DateTime DetermineEnd() { return _end = DateTime.Now; }
}