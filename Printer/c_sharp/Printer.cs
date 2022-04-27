using static System.Console;

namespace c_sharp;

/*
    Uses Laustrup conventions.
    
    Differences Console wise are:
    There is another order of printing.
    Can have different inputs.
    Is static.
    Can print the last output.
    Methods returns a string of print output.
 */

// Author Laust Eberhardt Bonnesen
public static class Printer
{
    // Fields and attributes
    public static int Version { get { return 101; } }
    private static string _border {get{return "---------------------------------------------------------------------";}}
    private static string _print { get; set; }
    public static string LastPrint
    {
        get
        {
            if (_print != null) { return _print; }
            else { return "There hasn't been printed anything yet..."; }
        }
    }

    // Methods that prints without change of print
    public static string WithLine(string content)
    {
        EditPrint(content);
        WriteLine(_print);
        return _print;
    }
    public static string WithoutLine(string content)
    {
        EditPrint(content);
        Write(_print);
        return _print;
    }
    
    // Methods that prints with change of print
    public static string Print(string content)
    {
        EditPrint("\n" + content + "\n\n");
        WriteLine(_print);
        return _print;
    }
    public static string Print(string content, Exception e)
    {
        EditPrint("\n" + _border + "\n" + content + "\n\n" + e + "\n\n" + e.StackTrace + "\n\n" + _border);
        WriteLine(_print);
        return _print;
    }
    public static string Print(object[] array)
    {
        EditPrint(array);
        Write(_print);
        return _print;
    }

    // Private methods
    private static void EditPrint(string content) { _print = content; }
    private static void EditPrint(object[] content)
    {
        _print = "{";
        for (int i = 0; i < content.Length; i++)
        {
            if (i!=content.Length-1) { _print += (string) content[i] + " - "; }
            else { _print += (string) content[i]; }
        }
        _print += "}";
    }
}