using Core.Domain.Enums;
using Core.Domain.State.Athletes;
using Core.Domain.State.Countries;
using EMS.Witness.Models;

namespace EMS.Witness.ConverterFactories;

public class AthleteFactory
{
    public static Athlete Create(NtsAthlete athlete)
    {
        var athleteState = new AthleteState
        {
            Category = Category.Seniors, //TODO: after athlete
            Club = athlete.Club?.Name,
            FeiId = athlete.FeiId, //TODO: after athlete
            FirstName = athlete.Names.GetFirstName(),
            LastName = athlete.Names.GetLastName(),
            Id = athlete.Id,
        };
        var country = new Country(athlete.Country?.IsoCode ?? "iso", athlete.Country?.Name ?? "country-name", 1337);
        return new Athlete(athleteState, country);
    }

    public Category MapCategory(NtsAthleteCategory category)
    {
        return category switch
        {
            NtsAthleteCategory.Senior => Category.Seniors,
            NtsAthleteCategory.Children => Category.Children,
            NtsAthleteCategory.JuniorOrYoungAdult => Category.JuniorOrYoungAdults,
            NtsAthleteCategory.Training or NtsAthleteCategory.Companion => Category.Seniors,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null),
        };
    }
}
