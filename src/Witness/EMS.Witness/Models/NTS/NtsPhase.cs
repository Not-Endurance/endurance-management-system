using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class NtsPhase : NtsAggregateRoot
{
    public static NtsPhase Create(Loop? loop, int? recovery, int? rest)
    {
        return new(loop, recovery, rest);
    }

    public static NtsPhase Update(int? id, Loop? loop, int? recovery, int? rest)
    {
        return new(id, loop, recovery, rest);
    }

    [JsonConstructor]
    public NtsPhase(int? id, Loop? loop, int? recovery, int? rest)
        : base(id!.Value)
    {
        Loop = Required(nameof(Loop), loop);
        Recovery = Required(nameof(Recovery), recovery);
        Rest = rest;
    }

    public NtsPhase(Loop? loop, int? recovery, int? rest)
        : this(GenerateId(), Required(nameof(Loop), loop), PositiveRecovery(recovery), NullOrPositiveRest(rest)) { }

    public Loop? Loop { get; private set; } // TODO: shouldnt be nullable probably
    public int Recovery { get; }
    public int? Rest { get; }

    public override string ToString()
    {
        var recovery = $"{Recovery}: {Recovery}";
        var rest = Rest != null ? $"rest: {Rest}" : null;
        return Combine(Loop, recovery, rest);
    }

    public void Reflect(Loop loop)
    {
        if (Loop == loop)
        {
            Loop = loop;
        }
    }

    static int PositiveRecovery(int? minutes)
    {
        return minutes ?? throw new Exception("typo");
    }

    static int? NullOrPositiveRest(int? minutes)
    {
        return minutes ?? throw new Exception("typo");
    }
}
