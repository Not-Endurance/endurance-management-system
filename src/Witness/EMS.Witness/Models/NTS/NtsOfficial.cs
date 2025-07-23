using EMS.Witness.Models.NTS;
using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class NtsOfficial : NtsAggregateRoot
{
    public static NtsOfficial Create(string? names, NtsOfficialRole? role)
    {
        return new(Person.Create(names), role);
    }

    public static NtsOfficial Update(int? id, string? names, NtsOfficialRole? role)
    {
        return new(id, Person.Create(names), role);
    }

    NtsOfficial(Person? person, NtsOfficialRole? role)
        : this(GenerateId(), person, role) { }

    [JsonConstructor]
    public NtsOfficial(int? id, Person? person, NtsOfficialRole? role)
        : base(id!.Value)
    {
        Role = Required(nameof(Role), role);
        Person = Required(nameof(Person), person);
    }

    public Person Person { get; }
    public NtsOfficialRole Role { get; }

    public override string ToString()
    {
        return Combine(Role.ToString(), Person);
    }

    public bool IsUniqueRole()
    {
        return Role
            is NtsOfficialRole.VeterinaryCommissionPresident
                or NtsOfficialRole.GroundJuryPresident
                or NtsOfficialRole.ForeignVeterinaryDelegate
                or NtsOfficialRole.TechnicalDelegate
                or NtsOfficialRole.ForeignJudge;
    }
}
