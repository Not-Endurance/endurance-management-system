using Core.Domain.AggregateRoots.Manager;
using Core.Domain.AggregateRoots.Manager.Aggregates.Participants;

namespace EMS.Witness.Models;

public class ProcessSnapshotsPayload
{
    public IEnumerable<ParticipantEntry> Entries { get; init; } = [];
    public WitnessEventType Type { get; init; }
}
