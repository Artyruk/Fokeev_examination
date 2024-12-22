//14.Поменять местами элементы строки с максимальной и минимальной суммой элементов.
using System.Runtime.InteropServices;
using static System.Console;

int m, n;
m = InputPositive("Введите кол-во строк m > 0    : ");
n = InputPositive("Введите кол-во столбцов n > 0 : ");
// работаем с матрицей
Matrix a = new Matrix(m, n);
a.Print("Исходная матрица:");
a.Swap();
a.Print("Поменяли местами строки");
// теперь в коллекциях
Lists l = new Lists(m, n);
l.Print("Исходная коллекция:");
l.Swap("Сумма элементов в строках");
l.Print("После смены строк");
static int InputPositive(string msg)
{
    int x = 0;
    bool cycle = true;
    while (cycle)
    {
        try
        {
            Write(msg);
            x = Convert.ToInt32(ReadLine());
            if (x > 0)
            {
                cycle = false;
            }
            else
            {
                throw new Exception("Введите число > 0 ! ");
            }
        }
        catch (Exception e)
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(e.Message);
            ResetColor();
        }
    }
    return x;
}

class Lists : Matrix
{
    // строки 
    public List<List<int>>? Rows { get; set; }
    // колонки
    public List<List<int>>? Columns { get; set; }
    // вся матрица в коллекции
    public List<int>? L { get; set; }

    public Lists(int m, int n) : base(m, n)
    {
        Matrix2Rows();
        Matrix2Columns();
        Matrix2List();
    }
    void Matrix2Rows()
    {
        Rows = new(m);
        for (int i = 0; i < m; i++)
        {
            List<int> TempR = new(n);
            for (int j = 0; j < n; j++)
            {
                TempR.Add(a[i, j]);
            }
            Rows.Add(TempR);
        }
    }
    void Matrix2Columns()
    {
        Columns = new(n);
        for (int j = 0; j < n; j++)
        {
            List<int> TempC = new(m);
            for (int i = 0; i < m; i++)
            {
                TempC.Add(a[i, j]);
            }
            Columns.Add(TempC);
        }
    }
    void Matrix2List()
    {
        L = new(m * n);
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                L.Add(a[i, j]);
            }
        }
    }
    public void Swap(string Header)
    {
        if (Rows == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        WriteLine(Header);
        List<int> arr = new List<int>(m);
        int imax = 0, imin = 0;
        for (int i = 0; i < m; i++)
        {
            arr.Add(Rows[i].Sum());
            WriteLine($"Сумма элементов в строке {i} равна {arr[i]}");
        }
        int max = arr[0], min = arr[0];
        for (int i = 0; i < m; i++)
        {
            if (arr[i] > max) imax = i;
            if (arr[i] < min) imin = i;
        }
        WriteLine($"Меняем местами строки {imin} и {imax}");
        List<int> temp = Rows[imin];
        Rows[imin] = Rows[imax];
        Rows[imax] = temp; 
        WriteLine();
    }

    public override void Print(string Header)
    {
        if (Rows == null)
        {
            WriteLine("Коллекция пуста");
            return;
        }
        WriteLine(Header);
        for (int i = 0; i < m; i++)
        {
            WriteLine(String.Format("{0,3}", i) + ") " +
                      String.Join(", ", Rows[i]));
        }
        WriteLine();
    }
}

class Matrix
{
    public int m { get; set; }
    public int n { get; set; }
    public int[,] a;
    readonly int max = 50;

    public Matrix(int m, int n)
    {
        this.m = m;
        this.n = n;
        Generate(m, n);
    }
    public void Generate(int m, int n)
    {
        a = new int[m, n]; // создаем пустой массив
        Random r = new();
        //заполняем массив данными
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                a[i, j] = r.Next(-max, max);
            }
        }
    }
    public void Clear()
    {
        a = new int[m, n]; // создаем пустой массив
    }
    public void Swap()
    {
        int[] summ = new int[m];
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                summ[i] += a[i, j];
            }
        }
        int max = summ[0];
        int min = summ[0];
        int imax = 0, imin = 0;
        for (int i = 0; i < m; i++)
        {
            if (max < summ[i])
            {
                max = summ[i];
                imax = i;
            }
            if (min > summ[i])
            {
                min = summ[i];
                imin = i;
            }
        }
        WriteLine($"В строке {imin} сумма минимальна = {summ[imin]}");
        WriteLine($"В строке {imax} сумма максимальна = {summ[imax]}");

        int temp = 0;
        for (int j = 0; j < n; j++)
        {
            temp = a[imin, j];
            a[imin, j] = a[imax, j];
            a[imax, j] = temp;
        }
    }
    public virtual void Print(string Header)
    {
        if (a == null)
        {
            WriteLine("Матрица пуста");
            return;
        }
        WriteLine(Header);
        Write("   ");
        for (int j = 0; j < n; j++)
        {
            Write(String.Format("{0,5}", j + ")"));
        }
        WriteLine();
        for (int i = 0; i < m; i++)
        {
            Write(String.Format("{0,3}", i + ")"));
            for (int j = 0; j < n; j++)
            {
                Write(String.Format("{0,5}", a[i, j]));
            }
            WriteLine();
        }
        WriteLine();
    }
}