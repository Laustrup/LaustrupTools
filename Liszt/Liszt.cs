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

    // Adds an amount of elements in front of the Liszt
    public T[] Add(T[] elements)
    {
        for (int i = 0; i < elements.Length; i++) { Add(elements[i]); }
        return _inventory;
    }
    // Adds an amount of elements in front of the Liszt
    public T[] Add(T element)
    {
        _inventory = CreateNewInventory(element);
        return _inventory;
    }
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
        _dictionary.Add("SPAREDATA",element);
        return _inventory;
    }

    public T Get(T element) { return _dictionary[element.ToString()]; }
    public T Get(string key) { return _dictionary[key]; }
    public T Get(int index) { return _inventory[index]; }
    public T GetDecremented(int index) { return _inventory[index+1];}
    public T GetSpareData() {return _dictionary["SPAREDATA"];}
    public T[] GetInventory() {return _inventory;}

    public void ClearAll() { InitiateAttributes(); }

    public bool Contains(T element)
    {
        for (int i = 0; i < _inventory.Length; i++) { if (_inventory.Equals(element)) { return true; } }
        return false;
    }
    public bool Contains(string key) { return _dictionary.ContainsKey(key); }
    
}