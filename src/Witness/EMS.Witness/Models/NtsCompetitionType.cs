using System.ComponentModel.DataAnnotations;

namespace EMS.Witness.Models;

public enum NtsCompetitionType // TODO: Use DisplayAttribute
{
    [Display(Name = "Qualification")]
    Qualification = 1,

    [Display(Name = "Star Level")]
    Star = 2,

    [Display(Name = "Championship")]
    Championship = 3,
}
