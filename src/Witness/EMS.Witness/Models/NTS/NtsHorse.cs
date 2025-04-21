namespace EMS.Witness.Models;

public class NtsHorse : NtsAggregateRoot
{
    public static NtsHorse Create(string? name, string? feiId)
    {
        return new(name, feiId);
    }

    public static NtsHorse Update(int? id, string? name, string? feiId)
    {
        return new(id, name, feiId);
    }

    NtsHorse(string? name, string? feiId)
        : this(GenerateId(), name, feiId) { }

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    public NtsHorse(int? id, string? name, string? feiId)
        : base(id!.Value)
    {
        Name = Required(nameof(Name), name);
        FeiId = feiId;
    }

    public string Name { get; }
    public string? FeiId { get; }

    public string Summarize()
    {
        return ToString();
    }

    public override string ToString()
    {
        return Name;
    }
}
