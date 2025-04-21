using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class NtsCombination : NtsAggregateRoot
{
    public static NtsCombination Create(int? number, NtsAthlete? athlete, NtsHorse? horse, Tag? tag)
    {
        return new(number, athlete, horse, tag);
    }

    public static NtsCombination Update(int? id, int? number, NtsAthlete? athlete, NtsHorse? horse, Tag? tag)
    {
        return new(id, number, athlete, horse, tag);
    }

    [JsonConstructor]
    public NtsCombination(int? id, int? number, NtsAthlete? athlete, NtsHorse? horse, Tag? tag)
        : base(id!.Value)
    {
        Number = Required(nameof(Number), number);
        Athlete = Required(nameof(Athlete), athlete);
        Horse = Required(nameof(Horse), horse);
        Tag = tag;
    }

    public NtsCombination(int? number, NtsAthlete? athlete, NtsHorse? horse, Tag? tag)
        : this(GenerateId(), number, athlete, horse, tag) { }

    public int Number { get; }
    public NtsAthlete Athlete { get; private set; }
    public NtsHorse Horse { get; private set; }
    public Tag? Tag { get; }

    public override string ToString()
    {
        var number = $"#{Number}";
        return Combine(number, Athlete, Horse);
    }
}
