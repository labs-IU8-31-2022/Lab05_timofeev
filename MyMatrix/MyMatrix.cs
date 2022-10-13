MyMatrix m1 = new(8, 6);
m1.Show();
m1.ShowPartialy(2,4,2,5);
m1.ChangeSize(6, 4);
m1.Show();
m1.ChangeSize(10, 12);
m1.Show();

class MyMatrix
{
    int[,] _matrix;
    private int _m;
    private int _n;
    private static int beginning;
    private static int ending;

    public MyMatrix()
    {
        _m = 0;
        _n = 0;
    }
    public MyMatrix(int m, int n)
    {
        if (m == 0 || n == 0)
        {
            Console.WriteLine("Неверно задано кол-во столбцов/строк");
            return;
        }

        _m = m;
        _n = n;
        _matrix = new int[_m, _n];
        Fill();
    }

    private void Range()
    {
        Console.WriteLine("Введите диапазон генерации чисел");
        beginning = Convert.ToInt32(Console.ReadLine());
        ending = Convert.ToInt32(Console.ReadLine());
    }

    private void Fill(int m = 0, int n = 0)
    {
        Random rand = new();
        if (beginning == 0 && ending == 0) Range();
        var tempM = m;
        var tempN = n;
        
        for (m = 0; m < _matrix.GetLength(0); ++m)
        {
            n = m >= tempM ? 0 : tempN;
            for (; n < _matrix.GetLength(1); ++n)
            {
                _matrix[m, n] = rand.Next(beginning, ending);
            }
        }
    }

    public void ChangeSize(int m, int n)
    {
        MyMatrix res = new();
        res._m = m;
        res._n = n;
        res._matrix = new int[m, n];
        for (int i = 0; i < _m && i < m; ++i)
        {
            for (int j = 0; j < _n && j < n; ++j)
            {
                res[i, j] = this[i, j];
            }
        }
        res.Fill(_m, _n);
        _matrix = res._matrix;
        _m = m;
        _n = n;
    }

    public void Show()
    {
        for (int i = 0; i < _matrix.GetLength(0); ++i, Console.WriteLine())
        {
            for (int j = 0; j < _matrix.GetLength(1); ++j)
            {
                Console.Write("{0,3}", this[i, j]);
            }
        }
        Console.WriteLine();
    }
    
    public void ShowPartialy(int m1, int m2, int n1, int n2)
    {
        var t = n1;
        for (; m1 <= m2; ++m1, Console.WriteLine())
        {
            for (n1 = t; n1 <= n2; ++n1)
            {
                Console.Write("{0,3}", this[m1, n1]);
            }
        }
        Console.WriteLine();
    }

    public int this[int indexM, int indexN]
    {
        get => _matrix[indexM, indexN];
        set => _matrix[indexM, indexN] = value;
    }
}