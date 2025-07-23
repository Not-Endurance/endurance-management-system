using Core.Models;

namespace EMS.Witness.Models.NTS;

public class UpcomingEventModel : IIdentifiable
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
    public string Place { get; init; } = default!;
    public List<NtsCompetition> Competitions { get; init; } = default!;
}
