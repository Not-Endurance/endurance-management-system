using Core.Domain.State.Horses;

namespace EMS.Witness.ConverterFactories;

public class HorseState : IHorseState
{
    public string FeiId { get; set; }
    public string Name { get; set; }
    public string Club { get; set; }
    public bool IsStallion { get; set; }
    public string Breed { get; set; }
    public string TrainerFeiId { get; set; }
    public string TrainerFirstName { get; set; }
    public string TrainerLastName { get; set; }
    public int Id { get; set; }
}
