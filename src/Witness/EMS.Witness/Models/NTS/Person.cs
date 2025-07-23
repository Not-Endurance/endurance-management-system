namespace EMS.Witness.Models.NTS;

public class Person
{
    public static Person? Create(string? names)
    {
        if (string.IsNullOrWhiteSpace(names))
        {
            return null;
        }
        return new Person { Names = names.Split(DELIMITER, StringSplitOptions.RemoveEmptyEntries) };
    }

    public static implicit operator string[](Person member)
    {
        return member.Names;
    }

    public static implicit operator Person(string[] names)
    {
        return new Person { Names = names };
    }

    public static implicit operator string(Person person)
    {
        return person.ToString();
    }

    internal static string DELIMITER = " ";

    public string[] Names { get; init; } = []; // TODO: consider encapsulating this;

    public override string ToString()
    {
        return string.Join(DELIMITER, Names);
    }

    public string? GetFirstName()
    {
        return Names.First();
    }

    public string? GetLastName()
    {
        return Names.Last();
    }
}
