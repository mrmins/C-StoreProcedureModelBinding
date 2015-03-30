using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class DataReaderT
{
    public static List<T> ReadStoredProceadures<T>(System.Data.Entity.DbContext dbContext, string storeName, List<Generic> ktv) where T : new()
    {
        var col = new List<T>();
        var type = typeof(T);
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var dr = DataReader.ReadStoredProceadures(dbContext, storeName, ktv);

        while (dr.Read())
        {
            var obj = new T();
            for (var i = 0; i < dr.FieldCount; i++)
            {
                var fieldName = dr.GetName(i);
                var prop = props.FirstOrDefault(x => x.Name.ToLower() == fieldName.ToLower());
                if (prop == null) continue;
                if (dr[i] == DBNull.Value) continue;
                switch (prop.PropertyType.Name.ToLower())
                    {
                    case "decimal":
                        prop.SetValue(obj, Convert.ToDecimal(dr[i]), null);
                        break;
                    case "string":
                        prop.SetValue(obj, Convert.ToString(dr[i]), null);
                        break;
                    case "int32":
                        prop.SetValue(obj, Convert.ToInt32(dr[i]), null);
                        break;
                    case "int64":
                        prop.SetValue(obj, Convert.ToInt64(dr[i]), null);
                        break;
                    case "byte":
                        prop.SetValue(obj, Convert.ToByte(dr[i]), null);
                        break;
                    default:
                        prop.SetValue(obj, dr[i], null);
                        break;
                    }
                }
                col.Add(obj);
            }
        dr.Close();
        return col;
    }
}
