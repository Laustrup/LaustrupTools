using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Liszt;

/*
    Uses Laustrup conventions.
    
    Differences array wise are:
    Index of array starts at 1 instead of 0. 0 is an index for spare data.
    Parameter inputs for initiating values are as T[]
    Has attributes of an array of T and as well an Dictionary. Dictionary's key is the toString of the object.
    That way an object of T can quickly be returned and the whole array can be looped thru easily.
 */

// Author Laust Eberhardt Bonnesen
public class Liszt<T>
{
    // Attributes
    public static long Version {get{return 101;}}

    private Dictionary<string,T> _dictionary { get; set; } 
    private T[] _inventory { get; set; }
    
    public int Size {get{return _inventory.Length-1;}}

    private string _spareDataKey {get{return "SPAREDATA";}}

    private void InitiateAttributes()
    {
        _inventory = new T[1];
        _dictionary = new Dictionary<string,T>();
    }
    
    // Constructors
    public Liszt() { InitiateAttributes(); }
    public Liszt(T[] initialElements)
    {
        InitiateAttributes();
        Add(initialElements);
    }

    // Add methods
    public T[] Add(T[] elements) { for (int i = 0; i < elements.Length; i++) { Add(elements[i]); } return _inventory; }
    public T[] Add(T element) { if (element!=null) {_inventory = CreateNewInventory(element);} return _inventory; }
    private T[] CreateNewInventory(T element)
    {
        // Temporally variable 
        T[] newInventory = _inventory;
        Array.Resize(ref newInventory,newInventory.Length+1);
        
        Console.WriteLine(newInventory);
        
        // Adding
        newInventory[_inventory.Length] = element;
        _dictionary.Add(DictionaryKey(element),element);

        Console.WriteLine(DictionaryKey(element));
        
        return newInventory;
    }
    private string DictionaryKey(T element)
    {
        if (element == null) {return "NULL";}
        else {return element.ToString();}
    }

    public T[] AddSpareData(T element)
    {
        _inventory[0] = element;
        _dictionary.Add(_spareDataKey,element);
        return _inventory;
    }

    // Get methods
    public T Get(T element) { return _dictionary[element.ToString()]; }
    public T Get(string key) { return _dictionary[key]; }
    public T Get(int index) { return _inventory[index]; }
    public T GetDecremented(int index) { return _inventory[index+1];}
    public T GetLastIndex() {return _inventory[_inventory.Length+1];}
    public T GetFirstIndex() {return _inventory[1];}
    public T GetSpareData() {return _dictionary[_spareDataKey];}
    public T[] GetInventory() {return _inventory;}

    // Boolean methods
    public bool FistIndexIs(T element) { return _inventory[1].Equals(element); }
    public bool FistIndexIs(object element) { return _inventory[1].Equals(element); }
    public bool LastIndexIs(T element) { return _inventory[Size].Equals(element); } 
    public bool LastIndexIs(object element) { return _inventory[Size].Equals(element); }

    public void ClearAll() { InitiateAttributes(); }

    public bool Contains(T element)
    {
        for (int i = 0; i < _inventory.Length; i++) { if (_inventory.Equals(element)) { return true; } }
        return false;
    }
    public bool Contains(string key) { return _dictionary.ContainsKey(key); }

    // Methods for showing indexes
    public void PrintIndexes()
    {
        Console.Write("{ ");
        for (int i = 1; i < _inventory.Length;i++)
        {
            T index = _inventory[i];

            try
            {
                if (!LastIndexIs(index)) {Console.Write(index.ToString() + " - ");}
                else {Console.Write(index.ToString());}
            }
            catch (Exception e) {Console.WriteLine(e);}
        }
        
        Console.Write(" }");
    }
    public override string ToString()
    {
        if (Size==0) {return "Liszt is empty...";}
        
        string result = "{ ";
        for (int i = 1; i < _inventory.Length;i++)
        {
            T index = _inventory[i];
            Type type = typeof(T).DeclaringType;

            try
            {
                if (!LastIndexIs(index)) { result += GetValue<T>(index.ToString()) + " - "; }
                else { result += GetValue<T>(index.ToString()); }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
        result += " }";

        return result;
    }
    
    private T GetValue<T>(object obj)
    {
        if (obj != null)
        {
            Type type = typeof(T);
 
            T value = default(T);
            var methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                where m.Name == "TryParse"
                select m).FirstOrDefault();
 
            if (methodInfo == null)
                throw new ApplicationException("Unable to find TryParse method!");

            object result = methodInfo.Invoke(null, new object[] { obj, value });
            if ((result != null) && ((bool)result))
            {
                methodInfo = (from m in type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    where m.Name == "Parse"
                    select m).FirstOrDefault();

                if (methodInfo == null)
                    throw new ApplicationException("Unable to find Parse method!");

                value = (T)methodInfo.Invoke(null, new object[] { obj });

                return (T)value;
            }
        }
 
        return default(T);
    }
    
}