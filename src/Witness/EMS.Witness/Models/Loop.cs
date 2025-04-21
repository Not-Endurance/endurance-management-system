using Newtonsoft.Json;

namespace EMS.Witness.Models;

public class Loop : AggregateRoot
{
    public static Loop Create(double? distance)
    {
        return new(distance);
    }

    public static Loop Update(int? id, double? distance)
    {
        return new(id, distance);
    }

    [JsonConstructor]
    public Loop(int? id, double? distance)
        : base(id!.Value)
    {
        Distance = PositiveDistance(distance);
    }

    public Loop(double? distance)
        : this(GenerateId(), distance) { }

    public double Distance { get; }

    public override string ToString()
    {
        return $"{Distance}km";
    }

    static double PositiveDistance(double? distance)
    {
        return distance ?? throw new Exception("typo");
    }
}
