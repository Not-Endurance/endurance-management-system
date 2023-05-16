﻿using System;

namespace EMS.Core.Domain.AggregateRoots.Manager
{
    public class WitnessEvent
    {
        public WitnessEventType Type { get; set; }
        public string TagId { get; set; }
        public DateTime Time { get; set; }
    }

    public enum WitnessEventType
    {
        Invalid = 0,
        VetIn = 1,
        Arrival = 2,
    }
}
