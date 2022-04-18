namespace Liszt;

/*
    Uses Laustrup conventions.
    Differences array wise are:
    DEFAULT_CAPACITY is 1
    Index of array starts at 1 instead of 0
    Parameter inputs for initiating values are as T[]
    Has attributes of an array of T and as well an Dictionary. Dictionary's key is the toString of the object.
    That way an object of T can quickly be returned and the whole array can be looped thru easily.
 */

public class Liszt<T>
{
    public static long Version {get{return 101;}}

    private Dictionary<string,T> _dictionaryData { get; set; } 
    private T[] _dataInventory { get; set; }
    
    private int _size { get; set; } public int Size {get{return _size;}}
    private int _identicalKeys { get; set; } public int IdenticalKeys { get{return _identicalKeys;} }

    public Liszt() { InitiateFields(); }
    public Liszt(T[] initialElements)
    {
        InitiateFields();
        Add(initialElements);
    }
    private void InitiateFields()
    {
        _size = 0;
        _dataInventory = new T[_size];
        _dictionaryData = new Dictionary<string,T>();
        _identicalKeys = 0;
    }

    // Adds an amount of elements in front of the Liszt
    public T[] Add(T[] elements)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            _dataInventory.Append(elements[i]);
            _dictionaryData.Add(CreateDictionaryKey(elements[i]),elements[i-_size]);
        }
        _size = _dataInventory.Length;

        return _dataInventory;
    }
    // Adds an amount of elements in front of the Liszt
    public T[] Add(T element)
    {
        _dataInventory.Append(element);
        _dataInventory.CopyTo(new T[]{element},_size);
        _dictionaryData.Add(CreateDictionaryKey(element),element);
        _size = _dataInventory.Length;

        return _dataInventory;
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
    
    public T Get(T element) { return _dictionaryData[element.ToString()]; }
    public T Get(string key) { return _dictionaryData[key]; }
    public T Get(int index) { return _dataInventory[index]; }
    public T[] GetInventory() {return _dataInventory;}

    public void ClearAll()
    {
        InitiateFields();
        _size = _dataInventory.Length;
    }

    public bool Contains(T element)
    {
        for (int i = 0; i < _size; i++)
        {
            if (_dataInventory.Equals(element))
            {
                return true;
            }
        }
        return false;
    }
    public bool Contains(string key) { return _dictionaryData.ContainsKey(key); }
    
}