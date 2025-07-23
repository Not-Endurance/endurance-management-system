using Core.Domain.AggregateRoots.Manager.Aggregates.Participants;
using EMS.Witness.Models;
using EMS.Witness.Models.NTS;

namespace EMS.Witness.ConverterFactories;

public class ParticipantEntryFactory
{
    public static ParticipantEntry Create(NtsParticipation participation, NtsCompetition competition)
    {
        var emsParticipation = ParticipationFactory.CreateEms(participation, competition);
        return new ParticipantEntry(emsParticipation);
    }
}
