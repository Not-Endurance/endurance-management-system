using System.Diagnostics.CodeAnalysis;

namespace EMS.Witness.Models;

public abstract class NtsAggregateRoot : IEquatable<NtsAggregateRoot>
{
    static readonly System.Random _random = new();
    static readonly HashSet<int> _uniqueIntegers = [];
    static readonly object LOCK = new();
    
    public static bool operator ==(NtsAggregateRoot? left, NtsAggregateRoot? right)
    {
        return left?.IsEqual(right) ?? right is null;
    }

    public static bool operator !=(NtsAggregateRoot? left, NtsAggregateRoot? right)
    {
        return !(left == right);
    }

    protected NtsAggregateRoot(int id)
    {
        Id = id;
    }

    // TODO: use DomainObject for ID, do private set
    public int Id { get; }

    protected static string Combine(params object?[] values)
    {
        var sections = values.Where(x => x != null);
        return string.Join(" | ", sections);
    }

    protected static int GenerateId()
    {
        lock (LOCK)
        {
            var id = _random.Next();
            while (_uniqueIntegers.Contains(id))
            {
                id = _random.Next();
            }

            _uniqueIntegers.Add(id);
            return id;
        }
    }

    protected static T NotDefault<T>(string field, T value)
        where T : struct
    {
        if (value.Equals(default))
        {
            throw new Exception("Typo");
        }
        return value;
    }

    protected static T Required<T>(string field, T? value)
        where T : struct
    {
        return value ?? throw new Exception("Typo");
    }

    [return: NotNull]
    protected static T Required<T>(string field, T? instance)
        where T : class
    {
        return instance ?? throw new Exception("Typo");
    }

    [return: NotNull]
    protected static string Required(string field, [NotNull] string? value)
    {
        return value ??= "Typo";
    }

    public bool Equals(NtsAggregateRoot? other)
    {
        return IsEqual(other);
    }

    public override bool Equals(object? other)
    {
        return IsEqual(other);
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public override string ToString()
    {
        throw new NotImplementedException($"'{GetType().Name}' has to override ToString() to provide short info");
    }

    bool IsEqual(object? other)
    {
        if (other is null or not NtsAggregateRoot)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        return GetHashCode() == other.GetHashCode() && GetType() == other.GetType();
    }
}
