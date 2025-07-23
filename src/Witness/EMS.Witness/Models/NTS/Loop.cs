using Core.Models;

namespace EMS.Witness.Models.NTS;

public class Loop : IIdentifiable
{
	public int Id { get; init; }

	public double Distance { get; init; }
}
