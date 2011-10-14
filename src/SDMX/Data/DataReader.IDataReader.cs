using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SDMX
{
    public partial class DataReader : IDataReader
    {
        void IDataReader.Close()
        {
            Dispose();
        }

        int IDataReader.Depth
        {
            get { return 0; }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            return GetTable().Clone();
        }

        bool IDataReader.IsClosed
        {
            get { return _disposed; }
        }

        bool IDataReader.NextResult()
        {
            return false;
        }

        bool IDataReader.Read()
        {
            return Read();
        }

        int IDataReader.RecordsAffected
        {
            get { return -1; }
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        int IDataRecord.FieldCount
        {
            get { return _record.Count; }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            return (bool)_record[GetTable().Columns[i].ColumnName];
        }

        byte IDataRecord.GetByte(int i)
        {
            return (byte)_record[GetTable().Columns[i].ColumnName];
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        char IDataRecord.GetChar(int i)
        {
            return (char)_record[GetTable().Columns[i].ColumnName];
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        IDataReader IDataRecord.GetData(int i)
        {
            return this;
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            return GetTable().Columns[i].DataType.Name;
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            return (DateTime)_record[GetTable().Columns[i].ColumnName];
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            return (decimal)_record[GetTable().Columns[i].ColumnName];
        }

        double IDataRecord.GetDouble(int i)
        {
            return (double)_record[GetTable().Columns[i].ColumnName];
        }

        Type IDataRecord.GetFieldType(int i)
        {
            return GetTable().Columns[i].DataType;
        }

        float IDataRecord.GetFloat(int i)
        {
            return (float)_record[GetTable().Columns[i].ColumnName];
        }

        Guid IDataRecord.GetGuid(int i)
        {
            return (Guid)_record[GetTable().Columns[i].ColumnName];
        }

        short IDataRecord.GetInt16(int i)
        {
            return (short)_record[GetTable().Columns[i].ColumnName];
        }

        int IDataRecord.GetInt32(int i)
        {
            return (int)_record[GetTable().Columns[i].ColumnName];
        }

        long IDataRecord.GetInt64(int i)
        {
            return (long)_record[GetTable().Columns[i].ColumnName];
        }

        string IDataRecord.GetName(int i)
        {
            return GetTable().Columns[i].ColumnName;
        }

        int IDataRecord.GetOrdinal(string name)
        {
            return GetTable().Columns[name].Ordinal;
        }

        string IDataRecord.GetString(int i)
        {
            return (string)_record[GetTable().Columns[i].ColumnName];
        }

        object IDataRecord.GetValue(int i)
        {
            string name = GetTable().Columns[i].ColumnName;
            return _record[name];
        }

        int IDataRecord.GetValues(object[] values)
        {
            int count = values.Length < _record.Count ? values.Length : _record.Count;

            int counter = 0;
            for (int i = 0; i < count; i++)
            {
                string name = GetTable().Columns[i].ColumnName;
                values[i] = _record[name];
                counter++;
            }

            return counter;
        }

        bool IDataRecord.IsDBNull(int i)
        {
            return _record[GetTable().Columns[i].ColumnName] == DBNull.Value;
        }

        object IDataRecord.this[string name]
        {
            get { return this[name]; }
        }

        object IDataRecord.this[int i]
        {
            get { return this[GetTable().Columns[i].ColumnName]; }
        }
    }
}
