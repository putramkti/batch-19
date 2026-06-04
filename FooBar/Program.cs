int n = 15;

List<string> result = new List<string>();
for (int i = 1; i <= n; i++)
{
    if (i % 3 == 0 && i % 5 == 0)
    {
        result.Add("FooBar");
    }
    else if (i % 3 == 0)
    {
        result.Add("Foo");
    }
    else if (i % 5 == 0)
    {
        result.Add("Bar");
    }
    else
    {
        result.Add(i.ToString());
    }

}
    String output = string.Join(", ", result);
    Console.WriteLine(output);