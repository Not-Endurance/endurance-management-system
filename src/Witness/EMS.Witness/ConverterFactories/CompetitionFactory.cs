using Core.Domain.Enums;
using Core.Domain.State.Competitions;
using EMS.Witness.Models;
using EMS.Witness.Models.NTS;

namespace EMS.Witness.ConverterFactories;

public class CompetitionFactory
{
    public static Competition Create(NtsCompetition ntsCompetition)
    {
        var laps = LapFactory.Create(ntsCompetition.Phases);
        var state = new CompetitionState
        {
            Id = ntsCompetition.Id,
            Name = ntsCompetition.Name,
            Type = MapCompetitionType(ntsCompetition.Ruleset),
        };
        var competition = new Competition(state);
        foreach (var lap in laps)
        {
            competition.Save(lap);
        }
        return competition;
    }

    public static CompetitionType MapCompetitionType(NtsCompetitionRuleset ruleset)
    {
        return ruleset switch
        {
            NtsCompetitionRuleset.Regional => CompetitionType.National,
            NtsCompetitionRuleset.FEI => CompetitionType.International,
            _ => throw new NotImplementedException(),
        };
    }

    public static NtsCompetitionRuleset MapCompetitionRuleset(CompetitionType emsCompetitionType)
    {
        return emsCompetitionType switch
        {
            CompetitionType.National => NtsCompetitionRuleset.Regional,
            CompetitionType.International => NtsCompetitionRuleset.FEI,
            _ => throw new NotImplementedException(),
        };
    }
}
