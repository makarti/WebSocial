using System.Data;
using Dapper;

namespace Core.Handlers;

public class SqlGuidTypeHandler : SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid guid)
    {
        parameter.Value = guid.ToString();
    }

    public override Guid Parse(object value)
    {
        if (value is Guid)
            return (Guid)value;

        return new Guid((string)value);
    }
}