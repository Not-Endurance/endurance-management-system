using Core.Domain.State.Participants;

namespace EMS.Witness.ConverterFactories;

public class ParticipantState : IParticipantState
{
    public bool Unranked { get; set; }
    public string Number { get; set; }
    public int? MaxAverageSpeedInKmPh { get; set; }
    public int Id { get; set; }
}
