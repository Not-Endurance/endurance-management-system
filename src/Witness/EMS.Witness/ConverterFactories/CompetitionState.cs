using Core.Domain.Enums;
using Core.Domain.State.Competitions;

namespace EMS.Witness.ConverterFactories;

public class CompetitionState : ICompetitionState
{
    public CompetitionType Type { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public string FeiCategoryEventNumber { get; set; }
    public string FeiScheduleNumber { get; set; }
    public string Rule { get; set; }
    public string EventCode { get; set; }
    public int Id { get; set; }
}
