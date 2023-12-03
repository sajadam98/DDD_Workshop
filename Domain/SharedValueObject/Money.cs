
public class Money : ValueObject
{
    public decimal Value { get; }
    public Money(decimal amount)
    {
        if (amount < 0) throw new InvalidOperationException("Money cannot be negative.");
        Value = amount;
    }

    public static Money operator -(Money left, Money right)
    {
        if (left < right)
            throw new InvalidOperationException(
                "First Value Can Not Be Grater Than Second Value!");
        return new Money(left.Value - right.Value);
    }

    public static Money operator +(Money left, Money right)
    => new Money(left.Value + right.Value);

    public static bool operator <(Money left, Money right)
    => left.Value < right.Value;

    public static bool operator >(Money left, Money right)
    => left.Value > right.Value;

    public static bool operator <=(Money left, Money right)
    => left.Value <= right.Value;

    public static bool operator >=(Money left, Money right)
    => left.Value >= right.Value;

    public static implicit operator Money(decimal amount)
    => new(amount);

    public Money Add(Money amountToAdd)
    => new Money(Value + amountToAdd.Value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}