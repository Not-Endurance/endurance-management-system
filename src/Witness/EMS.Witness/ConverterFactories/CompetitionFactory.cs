using Core.Domain.Enums;
using Core.Domain.State.Competitions;
using EMS.Witness.Models;

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

    public static CompetitionType MapCompetitionType(CompetitionRuleset ruleset)
    {
        return ruleset switch
        {
            CompetitionRuleset.Regional => CompetitionType.National,
            CompetitionRuleset.FEI => CompetitionType.International,
            _ => throw new NotImplementedException(),
        };
    }

    public static CompetitionRuleset MapCompetitionRuleset(CompetitionType emsCompetitionType)
    {
        return emsCompetitionType switch
        {
            CompetitionType.National => CompetitionRuleset.Regional,
            CompetitionType.International => CompetitionRuleset.FEI,
            _ => throw new NotImplementedException(),
        };
    }
}
