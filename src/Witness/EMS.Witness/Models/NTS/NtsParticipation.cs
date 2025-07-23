using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsParticipation : IIdentifiable
{
    public int Id { get; init; }
    public NtsCombination Combination { get; init; } = default!;
    public bool IsNotRanked { get; init; }
    public DateTimeOffset? StartTimeOverride { get; init; }
    public NtsParticipationCategory Category { get; init; }
    public double? MinAverageSpeed { get; init; }
    public double? MaxAverageSpeed { get; init; }
    public double? MaxSpeedOverride { get; init; }
}
