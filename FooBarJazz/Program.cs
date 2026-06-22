using System.Data;

MyClass myClass = new MyClass();
myClass.AddRule(3, "foo");
myClass.AddRule(4, "baz");
myClass.AddRule(5, "bar");
myClass.AddRule(7, "jazz");
myClass.AddRule(9, "huzz");

Console.Write("Input number: ");
string input = Console.ReadLine();

if (int.TryParse(input, out int n) && n > 0)
{
    myClass.Generator(n);
}
else
{
    Console.WriteLine("Please enter a valid positive integer!.");
}

public class MyClass
{
    private Dictionary<int, string> _rules = new Dictionary<int, string>();

    public void AddRule(int input, string output)
    {
        // if(_rules.TryAdd(input, output) && input > 0)
        // {

        // }
        _rules.Add(input, output);
    }

    public void Generator(int n)
    {
        List<string> result = new List<string>();
        for (int i = 1; i <= n; i++)
        {
            string value = "";

            // var _sortedRules = _rules.OrderBy(x => x.Key);
            foreach (var rule in _rules.OrderBy(x => x.Key))
            {
                if (i % rule.Key == 0)
                {
                    value += rule.Value;
                }
            }

            if (string.IsNullOrEmpty(value))
            {
                value = i.ToString();
            }

            result.Add(value);

        }

        string output = string.Join(", ", result);
        Console.WriteLine(output);
    }

}