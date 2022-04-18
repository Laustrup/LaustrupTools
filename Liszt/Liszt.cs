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
    private static long _serialVersion = 101; public static long SerialVersion { get; }

    private Dictionary<string,T> _dictionaryData { get; set; } 
    private T[] _elementData { get; set; }
    
    // Temporaily datas to create new data.
    private T[] _tempData { get; set; }
    private Dictionary<string,T> _tempDictionary { get; set; }

    private int _size { get; set; } public int Size {get{return _size;}}


    public Liszt()
    {
        _size = 0;
        InitiateFields();
    }
    public Liszt(T[] initialElements)
    {
        _size = initialElements.Length;
        Add(initialElements);
    }
    private void InitiateFields()
    {
        _elementData = new T[_size];
        _dictionaryData = new Dictionary<string,T>();
        _tempDictionary = new Dictionary<string, T>();
    }
    

    // Adds an amount of elements in front of the Liszt
    public T[] Add(T[] elements)
    {
        if (_dictionaryData == null || _elementData == null) { InitiateFields(); }

        _tempData = new T[_size + elements.Length];

        for (int i = 0; i < _tempData.Length; i++)
        {
            if (i > _size)
            {
                _tempData[i] = _elementData[i];
                _tempDictionary.Add(_elementData[i].ToString(),_elementData[i]);
            }
            else
            {
                _tempData[i] = elements[i-_size];
                _tempDictionary.Add(elements[i-_size].ToString(),elements[i-_size]);
            }
        }
        FinishAdd();

        return _elementData;
    }
    // Adds an amount of elements in front of the Liszt
    public T[] Add(T element)
    {
        if (_dictionaryData == null || _elementData == null) { InitiateFields(); }

        _tempData = new T[_size + 1];

        for (int i = 0; i < _tempData.Length; i++)
        {
            if (i > _size)
            {
                _tempData[i] = _elementData[i];
                _tempDictionary.Add(_elementData[i].ToString(),_elementData[i]);
            }
            else
            {
                _tempData[i] = element;
                _tempDictionary.Add(element.ToString(),element);
            }
        }
        FinishAdd();

        return _elementData;
    }

    private void FinishAdd()
    {
        _elementData = _tempData;
        _tempDictionary.Clear();
        _tempData = new T[0];
    }

}