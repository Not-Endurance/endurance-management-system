using Core.Domain.State.Horses;
using EMS.Witness.Models;
using EMS.Witness.Models.NTS;

namespace EMS.Witness.ConverterFactories;

public class HorseFactory
{
    public static Horse Create(NtsHorse horse)
    {
        var state = new HorseState { Name = horse.Name };
        return new Horse(state);
    }
}
