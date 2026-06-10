// int a = 1;
// Console.WriteLine(a);

// Console.WriteLine(a++);
// Console.WriteLine(a);

// int b = 1;
// Console.WriteLine(b);
// Console.WriteLine(++b);
// Console.WriteLine(b);


// Console.WriteLine(5/0);
// checked
// {
//     Console.WriteLine(5 / 0);
// }

// string s = null;
// string defaultValue = "default value";
// Console.WriteLine(s ??= defaultValue);


// int x = 1;
// Console.WriteLine($"value of x is {x}");

// string  a = null;
// string b = a ??= "default value";

// Console.WriteLine($"value of a is {a}");
// Console.WriteLine($"value of b is {b}");


// string[] words = null;
// string word = words[1];

// Console.WriteLine($"value of word is {word}");


// int a = int.MaxValue;
// int b = 2;
// int hasil = 0;
// int hasil  = checked(a + b);

// try
// {
//     // checked
//     // {
        
//         hasil = a + b; 
//     // }
//     Console.WriteLine($"Hasil penjumlahan: {hasil}");
// }
// catch (System.OverflowException ex)
// {
//     Console.WriteLine("Error: " + ex.Message);
// }


// Console.WriteLine(3/0);
// Console.WriteLine(3.0/0.0);

// decimal a = 1.0m;
// A a = new A(2){value = 3};
// Console.WriteLine(a.value);

// public class A
// {
//     public int value { get; init; }
//     public A(int value)
//     {
//         this.value = value;
//     }
// }

// SubClass1 obj1 = new SubClass1();
// obj1.Display();
// BaseClass obj2 = obj1;
// obj2.Display();

// Console.WriteLine("================================");

// SubClass2 obj3 = new SubClass2();
// obj3.Display();
// BaseClass obj4 = obj3;
// obj4.Display();

// Console.WriteLine("================================");

// SubClass3 obj5 = new SubClass3();
// obj5.Display();
// BaseClass obj6 = obj5;
// Console.WriteLine(5);
// obj6.Display();
// public class BaseClass
// {
//     public virtual void Display()
//     {
//         Console.WriteLine("BaseClass Display");
//     }
//     public override string ToString()
//     {
//         return "BaseClass ToString";
//     }
// }

// public class SubClass1 : BaseClass
// {
//     public override sealed void Display()
//     {
//         Console.WriteLine("SubClass1 Display");
//     }
// }

// public class SubClass2 : BaseClass
// {
//     public new void Display()
//     {
//         Console.WriteLine("SubClass2 Display");
//     }
// }

// public class SubClass3 : SubClass1
// {
//     public new void Display()
//     {
//         Console.WriteLine("SubClass3 Display");
//     }
// }



// DataUser user = new DataUser();

// Console.WriteLine(user.Id);      
// Console.WriteLine(user.Nama);    
// Console.WriteLine(user.IsAktif); 
// Console.WriteLine(user.Saldo);   
// public class DataUser
// {
//       private int _id;
    
//     public int Id => _id;
//     public string Nama { get; set; }
//     public bool IsAktif { get; set; }
//     public double Saldo { get; set; }
// }


// SubClass1 obj1 = new SubClass1();
// obj1.Display();
// obj1.cuy();
// // BaseClass obj2 = obj1;
// // obj2.Display();

// public class BaseClass
// {
//     public virtual void Display()
//     {
//         Console.WriteLine("BaseClass Display");
//     }
//     public override string ToString()
//     {
//         return "BaseClass ToString";
//     }
// }

// public class SubClass1 : BaseClass
// {
//     public override sealed void Display()
//     {
//         Console.WriteLine("SubClass1 Display");
//     }
//     public void cuy()
//     {
//         Console.WriteLine("SubClass1 cuy");
//     }
// }

// A a = new A();
// Console.WriteLine(a.Pa);
// public struct A
// {
//     public int Pa { get; set; }
//     public string Pb { get; set; }
// }


// string x = "5";
// int y = 10;
// Swap(ref x, ref y);

// static void Swap<T>(ref T a, ref T b)
// {
//     T temp = a;
//     a = b;
//     b = temp;
// }

// Orang o1 = new Orang{Nama = "John"};
// Console.WriteLine(o1.Nama);
Pegawai p1 = new Pegawai();

public class Orang
{
    public string Nama { get; set; }
    public Orang()
    {
        Console.WriteLine("haloow");
    }
}

public class Pegawai
{
    public Pegawai()
    {
        
    }
}