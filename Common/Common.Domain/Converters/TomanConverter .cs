using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Common.Domain.ValueObjects.Money;

public sealed class TomanConverter : ValueConverter<Toman, decimal>
{
    public TomanConverter()
        : base(v => v.Value, v => new Toman(v)) { }
}