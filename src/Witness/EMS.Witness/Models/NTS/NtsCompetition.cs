using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsCompetition : IIdentifiable
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public NtsCompetitionType Type { get; init; }
    public NtsCompetitionRuleset Ruleset { get; init; }
    public DateTimeOffset Start { get; init; }
    public List<NtsPhase> Phases { get; init; } = [];
    public List<NtsParticipation> Participations { get; init; } = [];

    public override string ToString()
    {
        return $"{Name} ({Phases.Count}) | {Type} | {Start:g}";
    }
}
