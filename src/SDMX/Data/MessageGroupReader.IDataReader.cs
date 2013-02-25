using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SDMX
{
    public partial class MessageGroupReader
    {
        void IDataReader.Close()
        {
            Dispose();
        }

        int IDataReader.Depth
        {
            get { return ((IDataReader)_reader).Depth; }
        }

        DataTable IDataReader.GetSchemaTable()
        {
            return ((IDataReader)_reader).GetSchemaTable();
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
            get { return ((IDataReader)_reader).RecordsAffected; }
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }

        int IDataRecord.FieldCount
        {
            get { return ((IDataReader)_reader).FieldCount; }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            return ((IDataReader)_reader).GetBoolean(i);
        }

        byte IDataRecord.GetByte(int i)
        {
            return ((IDataReader)_reader).GetByte(i);
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return ((IDataReader)_reader).GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        char IDataRecord.GetChar(int i)
        {
            return ((IDataReader)_reader).GetChar(i);
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return ((IDataReader)_reader).GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        IDataReader IDataRecord.GetData(int i)
        {
            return this;
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            return ((IDataReader)_reader).GetDataTypeName(i);
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            return ((IDataReader)_reader).GetDateTime(i);
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            return ((IDataReader)_reader).GetDecimal(i);
        }

        double IDataRecord.GetDouble(int i)
        {
            return ((IDataReader)_reader).GetDouble(i);
        }

        Type IDataRecord.GetFieldType(int i)
        {
            return ((IDataReader)_reader).GetFieldType(i);
        }

        float IDataRecord.GetFloat(int i)
        {
            return ((IDataReader)_reader).GetFloat(i);
        }

        Guid IDataRecord.GetGuid(int i)
        {
            return ((IDataReader)_reader).GetGuid(i);
        }

        short IDataRecord.GetInt16(int i)
        {
            return ((IDataReader)_reader).GetInt16(i);
        }

        int IDataRecord.GetInt32(int i)
        {
            return ((IDataReader)_reader).GetInt32(i);
        }

        long IDataRecord.GetInt64(int i)
        {
            return ((IDataReader)_reader).GetInt64(i);
        }

        string IDataRecord.GetName(int i)
        {
            return ((IDataReader)_reader).GetName(i);
        }

        int IDataRecord.GetOrdinal(string name)
        {
            return ((IDataReader)_reader).GetOrdinal(name);
        }

        string IDataRecord.GetString(int i)
        {
            return ((IDataReader)_reader).GetString(i);
        }

        object IDataRecord.GetValue(int i)
        {
            return ((IDataReader)_reader).GetValue(i);
        }

        int IDataRecord.GetValues(object[] values)
        {
            return ((IDataReader)_reader).GetValues(values);
        }

        bool IDataRecord.IsDBNull(int i)
        {
            return ((IDataReader)_reader).IsDBNull(i);
        }

        object IDataRecord.this[string name]
        {
            get { return ((IDataReader)_reader)[name]; }
        }

        object IDataRecord.this[int i]
        {
            get { return ((IDataReader)_reader)[i]; }
        }
    }
}
