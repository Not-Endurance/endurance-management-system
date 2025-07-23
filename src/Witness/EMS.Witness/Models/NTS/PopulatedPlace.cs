using System.Text;
using EMS.Witness.Models.NTS;

namespace EMS.Witness.Models;

public record PopulatedPlace
{
    public PopulatedPlace(NtsCountry country, string city, string? location)
    {
        Country = country;
        City = city;
        Location = location;
    }

    public NtsCountry Country { get; }
    public string City { get; }
    public string? Location { get; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        if (Location != null)
        {
            sb.Append($"{Location} ");
        }
        if (City != null)
        {
            sb.Append($"{City} ");
        }
        var country = Country.ToString();
        sb.Append(country);
        return sb.ToString();
    }
}
