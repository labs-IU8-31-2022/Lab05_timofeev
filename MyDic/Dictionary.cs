MyDictionary<string, int> d1 = new();
Console.WriteLine($"Capacity {d1.Capacity}");
Console.WriteLine($"Size {d1.Count}");
d1.Add("Vlad", 5);
Console.WriteLine($"Capacity {d1.Capacity}");
Console.WriteLine($"Size {d1.Count}");
foreach (var i in d1)
{
    Console.WriteLine($"Key: {i.Key} Value: {i.Value}");
}

d1.Add("Vlad", 7);
d1.Add("Vova", 5);
Console.WriteLine($"Capacity {d1.Capacity}");
Console.WriteLine($"Size {d1.Count}");
d1.Add("Danil", -12);
d1.Add("Dima", 0);
d1.Add("Denis", 10);
Console.WriteLine($"Capacity {d1.Capacity}");
Console.WriteLine($"Size {d1.Count}");
foreach (var i in d1)
{
    Console.WriteLine($"Key: {i.Key} Value: {d1[i.Key]}");
}

MyDictionary<int, int> d2 = new();
Console.WriteLine($"Capacity {d2.Capacity}");
Console.WriteLine($"Size {d2.Count}");
d2.Add(1, 5);
Console.WriteLine($"Capacity {d2.Capacity}");
Console.WriteLine($"Size {d2.Count}");
foreach (var i in d2)
{
    Console.WriteLine($"Key: {i.Key} Value: {i.Value}");
}

d2.Add(1, 7);
d2.Add(2, 5);
Console.WriteLine($"Capacity {d2.Capacity}");
Console.WriteLine($"Size {d2.Count}");
d2.Add(3, -12);
d2.Add(4, 0);
d2.Add(0, 4);

d2.Add(6, 10);
Console.WriteLine($"Capacity {d2.Capacity}");
Console.WriteLine($"Size {d2.Count}");

foreach (var i in d2)
{
    Console.WriteLine((uint)i.Key.GetHashCode() % d2.Capacity);
    Console.WriteLine($"Key: {i.Key} Value: {d2[i.Key]}");
}

class MyDictionary<TKey, TValue>
{
    private int[]? _buckets;
    private Entry[]? _entries;
    private int _capacity;
    private int _size;

    public int Count => _size;
    public int Capacity => _capacity;

    public MyDictionary(int capacity = 0)
    {
        if (capacity < 0)
            throw new Exception("negative capacity");

        _capacity = GetPrimeAbove(capacity);
        _size = 0;

        _buckets = new int[_capacity];
        _entries = new Entry[_capacity];
    }

    public void Add(TKey key, TValue value)
    {
        if (_size == _capacity)
        {
            _capacity = GetPrimeAbove(2 * _capacity + 1);
            _buckets = new int[_capacity];
            Entry[] temp = new Entry[_capacity];
            Array.Copy(_entries, temp, _size);
            _entries = temp;
            for (var i = 0; i < _size; ++i)
            {
                _buckets[_entries[i].hashCode % _capacity] = i;
            }
        }

        var elem = new Entry(key, value);
        if (_entries[_buckets[elem.hashCode % _capacity]].Key is not null
            && _entries[_buckets[elem.hashCode % _capacity]].Key.Equals(elem.Key))
        {
            _entries[_buckets[elem.hashCode % _capacity]] = elem;
            return;
        }

        if (_entries[_buckets[elem.hashCode % _capacity]].Key is not null
            && _entries[_buckets[elem.hashCode % _capacity]].hashCode.Equals(elem.hashCode))
        {
            _entries[_size] = elem;
            var index = _buckets[elem.hashCode % _capacity];
            while (_entries[index].next != -1)
            {
                index = _entries[index].next;
            }

            _entries[index].next = _size;
            ++_size;
            return;
        }

        _buckets[elem.hashCode % _capacity] = _size;
        _entries[_size] = elem;
        ++_size;
    }

    public TValue this[TKey? key]
    {
        get
        {
            var index = _buckets[(uint)key.GetHashCode() % _capacity];
            while (_entries[index].next != -1)
            {
                if (_entries[index].Key.Equals(key))
                    return _entries[index].Value;
                index = _entries[index].next;
            }

            if (_entries[index].Key.Equals(key))
                return _entries[index].Value;
            return default(TValue);
        }
        set
        {
            var index = _buckets[key.GetHashCode() % _capacity];
            while (!_entries[index].Key.Equals(key))
            {
                if (_entries[index].next != -1)
                {
                    index = _entries[index].next;
                    continue;
                }

                break;
            }

            if (_entries[index].Key.Equals(key))
            {
                _entries[index].Value = value;
            }

            if (key != null)
            {
                var keyn = (TKey)key;
                Add(keyn, value);
            }
        }
    }

    public IEnumerator<Entry> GetEnumerator()
    {
        for (var i = 0; i < _size; ++i)
        {
            yield return _entries[i];
        }
    }

    public struct Entry
    {
        public uint hashCode;
        public int next = -1;
        public TKey Key;
        public TValue Value;

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            hashCode = (uint)Key.GetHashCode();
        }
    }


    private static int GetPrimeAbove(int value)
    {
        for (var i = value;; i += 2)
            if (IsPrime(i))
                return i;
    }

    public static bool IsPrime(int value)
    {
        if (value <= 1)
            return false;
        if (value % 2 == 0)
            return value == 2;

        var n = (int)(Math.Sqrt(value) + 1);
        for (var i = 3; i <= n; i += 2)
            if (value % i == 0)
                return false;

        return true;
    }
}