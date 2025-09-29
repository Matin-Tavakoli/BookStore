using Common.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public sealed class PhoneNumberConverter : ValueConverter<PhoneNumber, string>
{
    public PhoneNumberConverter()
        : base(p => p.Value, s => new PhoneNumber(s)) { }
}