using Core.Domain.Enums;
using Core.Domain.State.Athletes;

namespace EMS.Witness.ConverterFactories;

internal class AthleteState : IAthleteState
{
    public string? FeiId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Club { get; set; }
    public Category Category { get; set; }
    public int Id { get; set; }
}
