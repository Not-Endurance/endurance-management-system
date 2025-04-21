namespace EMS.Witness.Models;

public class NtsAthlete : NtsAggregateRoot
{
    public static NtsAthlete Create(string? name, string? feiId, NtsCountry? country, Club? club, NtsAthleteCategory? category)
    {
        return new(Person.Create(name), feiId, country, club, category);
    }

    public static NtsAthlete Update(
        int? id,
        string? name,
        string? feiId,
        NtsCountry? country,
        Club? club,
        NtsAthleteCategory? category
    )
    {
        return new(id, Person.Create(name), feiId, country, club, category);
    }

    NtsAthlete(Person? person, string? feiId, NtsCountry? country, Club? club, NtsAthleteCategory? category)
        : this(GenerateId(), person, feiId, country, club, category) { }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public NtsAthlete(int? id, Person? names, string? feiId, NtsCountry? country, Club? club, NtsAthleteCategory? category)
        : base(id!.Value)
    {
        FeiId = feiId;
        Names = Required(nameof(Names), names);
        Country = Required(nameof(Country), country);
        Category = Required(nameof(Category), category);
        Club = club;
    }

    public string? FeiId { get; }
    public Person Names { get; }
    public NtsCountry Country { get; }
    public Club? Club { get; private set; }
    public NtsAthleteCategory Category { get; private set; }

    public override string ToString()
    {
        return Names.ToString();
    }

    public void Reflect(Club club)
    {
        if (Club == club)
        {
            Club = club;
        }
    }
}
