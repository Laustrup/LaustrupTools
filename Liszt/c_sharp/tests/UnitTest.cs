using tools;
using Xunit.Abstractions;

namespace tests;

public abstract class UnitTest
{
    protected DateTime _start { get; set; }
    protected DateTime _end { get; set; }
    
    protected readonly ITestOutputHelper _output;
    
    public UnitTest(ITestOutputHelper output) {_output = output;}

    protected TimeSpan End()
    {
        _end = DateTime.Now;
        TimeSpan performanceTime = _end.Subtract(_start);
        Printer.Print("\tPerformance time = " + performanceTime.TotalMinutes);
        _output.WriteLine(Printer.LastPrint);
        return performanceTime;
    }

    protected DateTime Start()
    {
        _start = DateTime.Now;
        return _start;
    }
}