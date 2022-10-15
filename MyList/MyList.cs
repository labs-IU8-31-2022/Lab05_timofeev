MyList<int> t1 = new(3);
Console.WriteLine($"Elements {t1.Count}");
Console.WriteLine($"Capacity {t1.Capacity}");
t1.Add(3);
t1.Add(5);
t1.Add(0);
Console.WriteLine($"Elements {t1.Count}");
Console.WriteLine($"Capacity {t1.Capacity}");
t1.Add(7);
Console.WriteLine($"Elements {t1.Count}");
Console.WriteLine($"Capacity {t1.Capacity}");
foreach (var i in t1)
{
    Console.Write("{0,3}", i);
}

Console.WriteLine();
MyList<int> t2 = new(5, 3, 3, 2, 5);
Console.WriteLine($"Elements {t2.Count}");
Console.WriteLine($"Capacity {t2.Capacity}");
for (var i = 0; i < t2.Count; ++i)
{
    Console.Write("{0,3}", t2[i]);
}


class MyList<T>
{
    private T[] _items;
    private int _size;

    public MyList()
    {
        _items = Array.Empty<T>();
    }

    public MyList(int capacity)
    {
        if (capacity < 0)
            throw new Exception("Negative capacity");

        if (capacity == 0)
            _items = Array.Empty<T>();

        else
            _items = new T[capacity];
    }

    public MyList(params T[] items)
    {
        if (items is ICollection<T> c)
        {
            var count = c.Count;

            if (count == 0)
                _items = Array.Empty<T>();

            else
            {
                _items = new T[count];
                Array.Copy(items, _items, count);
                _size = count;
            }
        }
    }

    public int Capacity
    {
        get => _items.Length;
        set
        {
            if (value < _size)
            {
                throw new Exception("New capacity is smaller than size of List");
            }

            if (value != _items.Length)
            {
                if (value > 0)
                {
                    T[] itemsNew = new T[value];
                    if (_size > 0)
                    {
                        Array.Copy(_items, itemsNew, _size);
                    }

                    _items = itemsNew;
                }
                else
                {
                    _items = Array.Empty<T>();
                }
            }
        }
    }

    public int Count => _size;

    public void Add(T item)
    {
        if (_size < _items.Length)
        {
            _items[_size] = item;
            ++_size;
        }
        else
        {
            Capacity = _items.Length == 0 ? 4 : 2 * _items.Length;
            _items[_size] = item;
            ++_size;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; ++i)
        {
            yield return _items[i];
        }
    }

    public T this[int index]
    {
        get => _items[index];
        set => _items[index] = value;
    }
}