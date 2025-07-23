using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsCombination : IIdentifiable
{
    public int Id { get; init; }
    public int Number { get; init; }
    public NtsAthlete Athlete { get; init; } = default!;
    public NtsHorse Horse { get; init; } = default!;
}
