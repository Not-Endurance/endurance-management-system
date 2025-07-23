using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsPhase : IIdentifiable
{
    public int Id { get; init; }
    public Loop? Loop { get; init; }
    public int Recovery { get; init; }
    public int? Rest { get; init; }
}
