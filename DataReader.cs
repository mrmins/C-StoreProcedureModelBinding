using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public static class DataReader
{
    public static System.Data.Common.DbDataReader ReadStoredProceadures(System.Data.Entity.DbContext dbContext, string commandText, List<Generic> parameters )
    {
        dbContext.Database.Connection.Close();
        dbContext.Database.Connection.Open();
        var command = dbContext.Database.Connection.CreateCommand();
        command.CommandText = commandText;
        foreach (var item in parameters)
        {
            command.Parameters.Add(new SqlParameter
            {
                ParameterName = item.Key,
                SqlDbType = item.Type,
                SqlValue = item.Value
            });
        }
        command.CommandType = CommandType.StoredProcedure;
        return command.ExecuteReader();
    }
}
