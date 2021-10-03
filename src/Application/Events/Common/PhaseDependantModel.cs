﻿using EnduranceJudge.Domain.State.Phases;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Events.Common
{
    public class PhaseDependantModel : IPhaseState
    {
        public int Id { get; set; }
        public int LengthInKm { get; set; }
        public bool IsFinal { get; set; }
        public int OrderBy { get; set;  }
        public IEnumerable<PhaseForCategoryDependantModel> PhasesForCategories { get; set; }
            = new List<PhaseForCategoryDependantModel>();
    }
}
