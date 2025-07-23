using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsAthlete : IIdentifiable
{
    public int Id { get; init; }
    public string? FeiId { get; init; }
    public Person Names { get; init; } = default!;
    public NtsCountry Country { get; init; } = default!;
    public Club? Club { get; init; }
    public NtsParticipationCategory Category { get; init; }
}
