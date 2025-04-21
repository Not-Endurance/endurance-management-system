using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class NtsParticipation : NtsAggregateRoot
{
    const double CHILDREN_MIN_SPEED = 8;
    const double CHILDREN_MAX_SPEED = 12;
    const double MIN_SPEED = 10;
    const double MAX_SPEED = 16;

    public static NtsParticipation Create(
        DateTimeOffset? newStart,
        bool isUnranked,
        NtsCombination? combination,
        double? maxSpeedOverride
    )
    {
        return new(newStart, isUnranked, combination, maxSpeedOverride);
    }

    public static NtsParticipation Update(
        int? id,
        DateTimeOffset? newStart,
        bool isUnranked,
        NtsCombination? combination,
        double? maxSpeedOverride
    )
    {
        return new(id, newStart, isUnranked, combination, maxSpeedOverride);
    }

    [JsonConstructor]
    NtsParticipation(
        int? id,
        DateTimeOffset? startTimeOverride,
        bool isUnranked,
        NtsCombination? combination,
        double? maxSpeedOverride
    )
        : base(id!.Value)
    {
        StartTimeOverride = startTimeOverride;
        IsNotRanked = isUnranked;
        Combination = Required(nameof(Combination), combination);
        MaxSpeedOverride = maxSpeedOverride;
    }

    NtsParticipation(
        DateTimeOffset? startTimeOverride,
        bool isUnranked,
        NtsCombination? combination,
        double? maxSpeedOverride
    )
        : this(GenerateId(), IsFutureTime(startTimeOverride), isUnranked, combination, maxSpeedOverride) { }

    public NtsCombination Combination { get; private set; }
    public bool IsNotRanked { get; }
    public DateTimeOffset? StartTimeOverride { get; }
    public double? MinAverageSpeed { get; private set; }
    public double? MaxAverageSpeed { get; private set; }
    public double? MaxSpeedOverride { get; private set; }

    internal void SetSpeedLimits(NtsCompetitionType competitionType)
    {
        var athleteCategory = Combination.Athlete.Category;
        MinAverageSpeed = MIN_SPEED;
        if (competitionType == NtsCompetitionType.Qualification)
        {
            if (athleteCategory == NtsAthleteCategory.Children)
            {
                MinAverageSpeed = CHILDREN_MIN_SPEED;
                MaxAverageSpeed = CHILDREN_MAX_SPEED;
            }
            else
            {
                MaxAverageSpeed = MAX_SPEED;
            }
        }
        if (MaxSpeedOverride != null)
        {
            MaxAverageSpeed = MaxSpeedOverride;
        }
    }

    public override string ToString()
    {
        var startTimeMessage =
            StartTimeOverride != null ? $"start: {StartTimeOverride.Value.ToLocalTime().TimeOfDay} " : null;
        var isUnrankedMessage = IsNotRanked ? "not ranked" : null;
        return Combine(Combination, startTimeMessage, isUnrankedMessage);
    }

    static DateTimeOffset? IsFutureTime(DateTimeOffset? startTimeOverride)
    {
        return startTimeOverride;
    }
}
