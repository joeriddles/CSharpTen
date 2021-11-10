using System.Runtime.CompilerServices;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// var person = new Person(null);
var person = new Person("Tod");
Console.WriteLine(person.Name);

var name = $"{"T" + "o" + "d"}";
Verify.AssertNotNullOrEmpty($"{"T" + "o" + "d"}");
Verify.AssertNotNullOrEmpty(person.Name);

person = new Person(name); // Why does this still print "argumentExpression = value" ??
Console.WriteLine(person.Name);

name = new Random().Next().ToString(); // Maybe this will print something different...
person = new Person($"name = {name}");

var otherPerson = person.WithLastName("Smith");
Console.WriteLine($"{otherPerson.Name} {otherPerson.LastName}");


static class Verify
{
    public static string AssertNotNullOrEmpty(
        string? argument,
        [CallerArgumentExpression("argument")]
        string argumentExpression = null!
    )
    {
        if (argument is null || argument == "")
            throw new ArgumentNullException(nameof(argument));

        Console.WriteLine($"argument: {argument}, argumentExpression: {argumentExpression}");
        return argument;
    }
}

record Person
{
    public Person(string name) { Name = name; }
    public string Name {
        get => _Name!;
        set => _Name = Verify.AssertNotNullOrEmpty($"value = {value}"); }
    private string? _Name;
    
    public string LastName {
        get => _LastName!;
        set => _LastName = Verify.AssertNotNullOrEmpty(value); }
    private string? _LastName;

    public Person WithLastName(string lastName) // When I change `Person` from `record` to `class`, this breaks... ??
    {
        return this with
        {
            LastName = lastName,
        };
    }
}

