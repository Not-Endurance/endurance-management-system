using Core.Models;
using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class UpcomingEvent : AggregateRoot, IIdentifiable
{
    public static UpcomingEvent Create(string? name, string? place, Country? country, string? showFeiId)
    {
        return new(name, place, country, showFeiId);
    }

    public static UpcomingEvent Update(
        int? id,
        string? name,
        string? place,
        Country? country,
        string? showFeiId,
        IEnumerable<Competition> competitions,
        IEnumerable<Official> officials,
        IEnumerable<Loop> loops,
        IEnumerable<Combination> combinations)
    {
        return new(id, name, place, country, showFeiId, competitions, officials, loops, combinations);
    }

    readonly List<Competition> _competitions = [];
    readonly List<Official> _officials = [];
    readonly List<Loop> _loops = [];
    readonly List<Combination> _combinations = [];

    [JsonConstructor]
    UpcomingEvent(
        int? id,
        string? name,
        string? place,
        Country? country,
        string? showFeiId,
        IEnumerable<Competition> competitions,
        IEnumerable<Official> officials,
        IEnumerable<Loop> loops,
        IEnumerable<Combination> combinations)
        : base(id!.Value)
    {
        Name = Required(nameof(Name), name);
        Place = Capitalized(nameof(Place), place);
        Country = Required(nameof(Country), country);
        ShowFeiId = showFeiId;
        _competitions = competitions.ToList();
        _officials = officials.ToList();
        _loops = loops.ToList();
        _combinations = combinations.ToList();
    }

    UpcomingEvent(string? name, string? place, Country? country, string? showFeiId)
        : this(GenerateId(), name, place, country, showFeiId, [], [], [], []) { }

    public string Name { get; }
    public string Place { get; }
    public Country Country { get; }
    public string? ShowFeiId { get; }
    public IReadOnlyList<Competition> Competitions => _competitions.AsReadOnly();
    public IReadOnlyList<Official> Officials => _officials.AsReadOnly();
    public IReadOnlyList<Loop> Loops => _loops.AsReadOnly();
    public IReadOnlyList<Combination> Combinations => _combinations.AsReadOnly();
    
    public void Remove(Official official)
    {
        _officials.Remove(official);
    }

    public override string ToString()
    {
        return Combine(Name, Place, Country);
    }

    static string Capitalized(string name, string? value)
    {
        Required(name, value);

        var character = value.First();
        return value;
    }

    void ValidateRole(Official member)
    {
        var role = member.Role;
        if (!member.IsUniqueRole())
        {
            return;
        }
        var existing = _officials.FirstOrDefault(x => x.Role == role);
        if (existing == null || existing == member)
        {
            return;
        }
    }
}
