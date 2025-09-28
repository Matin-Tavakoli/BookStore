using System.Text.RegularExpressions;
using Common.Domain.Exceptions;

namespace Common.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    private static readonly Regex Pattern = new(@"^09\d{9}$", RegexOptions.Compiled);

    public string Value { get; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidDomainDataException("شماره تلفن خالی است");

        if (!Pattern.IsMatch(value))
            throw new InvalidDomainDataException("شماره تلفن نامعتبر است");

        Value = value;
    }

    public override string ToString() => Value;
}