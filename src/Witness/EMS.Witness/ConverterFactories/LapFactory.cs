using Core.Domain.State.Laps;
using EMS.Witness.Models;

namespace EMS.Witness.ConverterFactories;

public class LapFactory
{
    public static IEnumerable<Lap> Create(IEnumerable<NtsPhase> phases)
    {
        var i = 0;
        foreach (var phase in phases)
        {
            var state = new LapState
            {
                Id = phase.Id,
                IsFinal = phases.Last() == phase,
                IsCompulsoryInspectionRequired = false,
                LengthInKm = phase.Loop!.Distance,
                MaxRecoveryTimeInMins = phase.Recovery,
                OrderBy = ++i,
                RestTimeInMins = phase.Rest ?? 0,
            };
            yield return new Lap(state);
        }
    }
}
