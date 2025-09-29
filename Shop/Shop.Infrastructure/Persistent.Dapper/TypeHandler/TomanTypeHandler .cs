using Dapper;
using System.Data;
using Common.Domain.ValueObjects.Money;

public class TomanTypeHandler : SqlMapper.TypeHandler<Toman>
{
    public override void SetValue(IDbDataParameter parameter, Toman value)
    {
        parameter.Value = value.Value; // ذخیره به عنوان decimal
        parameter.DbType = DbType.Decimal;
    }

    public override Toman Parse(object value)
    {
        return new Toman(Convert.ToDecimal(value));
    }
}