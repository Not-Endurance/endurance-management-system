using Core.Models;

namespace EMS.Witness.Models.NTS;

public class Club : IIdentifiable
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;
}
