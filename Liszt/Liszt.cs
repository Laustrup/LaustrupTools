namespace Liszt;

/*
    Uses Laustrup conventions.
    Differences array wise are:
    Index of array starts at 1 instead of 0. 0 is an index for spare data.
    Parameter inputs for initiating values are as T[]
    Has attributes of an array of T and as well an Dictionary. Dictionary's key is the toString of the object.
    That way an object of T can quickly be returned and the whole array can be looped thru easily.
 */

public class Liszt<T>
{
    public static long Version {get{return 101;}}

    private Dictionary<string,T> _dictionary { get; set; } 
    private T[] _inventory { get; set; }
    
    public int Size {get{return _inventory.Length-1;}}
    private int _identicalKeys { get; set; } public int IdenticalKeys { get{return _identicalKeys;} }

    public Liszt() { InitiateFields(); }
    public Liszt(T[] initialElements)
    {
        InitiateFields();
        Add(initialElements);
    }
    private void InitiateFields()
    {
        _inventory = new T[1];
        _dictionary = new Dictionary<string,T>();
        _identicalKeys = 0;
    }

    // Adds an amount of elements in front of the Liszt
    public T[] Add(T[] elements)
    {
        T[] newInventory = _inventory;
        Array.Resize(ref newInventory,_inventory.Length+elements.Length);
        
        for (int i = 0; i < elements.Length; i++)
        {
            newInventory[_inventory.Length + i] = elements[i];
            _dictionary.Add(CreateDictionaryKey(elements[i]),elements[i-Size]);
        }
        _inventory = newInventory;

        return _inventory;
    }
    // Adds an amount of elements in front of the Liszt
    public T[] Add(T element)
    {
        T[] newInventory = _inventory;
        Array.Resize(ref newInventory,_inventory.Length+1);
        
        newInventory[_inventory.Length] = element;
        _dictionary.Add(CreateDictionaryKey(element),element);

        _inventory = newInventory;
        
        return _inventory;
    }

    public T[] AddSpareData(T element)
    {
        _inventory[0] = element;
        _dictionary.Add("SpareData",element);
        return _inventory;
    }

    private string CreateDictionaryKey(T element)
    {
        if (Contains(element.ToString()))
        {
            _identicalKeys++;
            return "This is identical key number " + _identicalKeys;
        }
        return element.ToString();
    }
    
    public T Get(T element) { return _dictionary[element.ToString()]; }
    public T Get(string key) { return _dictionary[key]; }
    public T Get(int index) { return _inventory[index]; }
    public T GetDecremented(int index) { return _inventory[index+1];}
    public T GetSpareData() {return _dictionary["SpareData"];}
    public T[] GetInventory() {return _inventory;}

    public void ClearAll() { InitiateFields(); }

    public bool Contains(T element)
    {
        for (int i = 0; i < _inventory.Length; i++) { if (_inventory.Equals(element)) { return true; } }
        return false;
    }
    public bool Contains(string key) { return _dictionary.ContainsKey(key); }
    
}