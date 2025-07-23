using Core.Models;

namespace EMS.Witness.Models.NTS;

public class NtsCountry : IIdentifiable
{
    public int Id { get; init; }
    public string IsoCode { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string? NfCode { get; init; }
    public string? Locale { get; init; }

    public override string ToString()
    {
        return Name;
    }
}
