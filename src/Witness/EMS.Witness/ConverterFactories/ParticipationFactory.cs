using Core.Domain.State.LapRecords;
using Core.Domain.State.Participants;
using Core.Domain.State.Participations;
using EMS.Witness.Models;

namespace EMS.Witness.ConverterFactories;

public class ParticipationFactory
{
    public static Participation CreateEms(NtsParticipation ntsParticipation, NtsCompetition ntsCompetition)
    {
        var athlete = AthleteFactory.Create(ntsParticipation.Combination.Athlete);
        var horse = HorseFactory.Create(ntsParticipation.Combination.Horse);

        var state = new ParticipantState
        {
            Number = ntsParticipation.Combination.Number.ToString(),
            MaxAverageSpeedInKmPh = (int?)ntsParticipation.MinAverageSpeed,
            Unranked = true, // Cannot be fixed easy because IsNotRanked is on Ranking level. Not necessary in current Witness
        };
        var emsParticipant = new Participant(athlete, horse, state);
        // var emsLaps = LapFactory.Create(ntsParticipation.Phases).ToList();
        // for (var i = 0; i < ntsParticipation.Phases.Length; i++)
        // {
        //     var phase = ntsParticipation.Phases[i];
        //     var emsLap = emsLaps[i];
        //     if (phase.StartTime == null)
        //     {
        //         break;
        //     }
        //     var emsRecord = new LapRecord(phase.StartTime.Value.Date, emsLap)
        //     {
        //         ArrivalTime = phase.ArriveTime?.DateTime,
        //         InspectionTime = phase.PresentTime?.DateTime,
        //         ReInspectionTime = phase.RepresentTime?.DateTime,
        //     };
        //     emsParticipant.Add(emsRecord);
        // }
        var competition = CompetitionFactory.Create(ntsCompetition);

        return new Participation(emsParticipant, competition);
    }
}
