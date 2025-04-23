using Core.Domain.AggregateRoots.Manager;
using Core.Domain.AggregateRoots.Manager.Aggregates.Participants;
using EMS.Witness.Rpc;

namespace EMS.Witness.Models;

public class ProcessSnapshotsPayload
{
    public IEnumerable<EmsSnapshotModel> Entries { get; init; } = [];
    public WitnessEventType Type { get; init; }
}
