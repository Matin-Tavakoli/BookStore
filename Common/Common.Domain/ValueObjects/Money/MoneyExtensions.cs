namespace Common.Domain.ValueObjects.Money;

public static class MoneyExtensions
{
    public static Toman SumToman<T>(this IEnumerable<T> source, Func<T, Toman> selector)
    {
        return source.Aggregate(Toman.Zero, (acc, x) => acc + selector(x));
    }
}
