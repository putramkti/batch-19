Console.Write("Input number: ");
int n = Convert.ToInt32(Console.ReadLine());

List<string> result = new List<string>();
for (int i = 1; i <= n; i++)
{
    string value = "";

    if (i % 3 == 0)
    {
        value += "foo";
    }
    if (i % 5 == 0)
    {
        value += "bar";
    }
    if (i % 7 == 0)
    {
        value += "jazz";
    }
    if (string.IsNullOrEmpty(value))
    {
        value = i.ToString();
    }

    result.Add(value);

}

String output = string.Join(", ", result);
Console.WriteLine(output);