using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsHorse : IIdentifiable
{
    public int Id { get; init; }

    public string Name { get; init; } = default!;
    public string? FeiId { get; init; }
}
