namespace Common.Domain.ValueObjects.Money;

public class Toman : ValueObject
{
    public decimal Value { get; }
    public static Toman Zero => new Toman(0m);

    public Toman(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Price cannot be negative.");
        Value = value;
    }

    public static Toman operator +(Toman a, Toman b) => new Toman(a.Value + b.Value);
    public static Toman operator -(Toman a, Toman b) => new(a.Value - b.Value);
    public static Toman operator *(Toman a, int factor) => new(a.Value * factor);
    public static Toman operator *(Toman a, decimal factor) => new(a.Value * factor);
    public static Toman operator /(Toman a, int divisor) => new(a.Value / divisor);
    public static Toman operator /(Toman a, decimal divisor) => new(a.Value / divisor);
    public override string ToString() => $"{Value:N0} تومان";
}
