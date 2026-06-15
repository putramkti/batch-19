Console.Write("Input number: ");
string input = Console.ReadLine();

if (int.TryParse(input, out int n) && n > 0)
{
    List<string> result = new List<string>();
    for (int i = 1; i <= n; i++)
    {
        string value = "";

        if (i % 3 == 0)
        {
            value += "foo";
        }
        if (i % 4 == 0)
        {
            value += "baz";
        }
        if (i % 5 == 0)
        {
            value += "bar";
        }
        if (i % 7 == 0)
        {
            value += "jazz";
        }
        if( i % 9 == 0)
        {
            value += "huzz";
        }
        if (string.IsNullOrEmpty(value))
        {
            value = i.ToString();
        }

        result.Add(value);

    }

    String output = string.Join(", ", result);
    Console.WriteLine(output);
}
else
{
    Console.WriteLine("Please enter a valid positive integer!.");
}