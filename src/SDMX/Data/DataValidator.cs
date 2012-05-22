using System;
using Common;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SDMX
{
    public class DataValidator
    {
        Dictionary<string, bool> _keys = new Dictionary<string, bool>();

        public DataValidator(KeyFamily keyFamily)
        {
            KeyFamily = keyFamily;
        }

        public KeyFamily KeyFamily { get; private set; }

        public IEnumerable<Error> Validate(IDataReader reader)
        {            
            while (reader.Read())
            {
                foreach (var error in ValidateRecord(reader))
                {
                    yield return error;
                }
            }
        }

        /// <summary>
        /// Validates a record.
        /// </summary>
        /// <param name="record">The record to validate.</param>        
        /// <returns>Returns a list of errors if the record is not valid; otherwise an empty list.</returns>
        public List<Error> ValidateRecord(IDataRecord record)
        {
            var dict = GetDict(record);
            var values = new Dictionary<string, string>();
            return ValidateRecord(dict, out values);
        }

        /// <summary>
        /// Validates a record.
        /// </summary>
        /// <param name="record">The record to validate.</param>        
        /// <returns>Returns a list of errors if the record is not valid; otherwise an empty list.</returns>
        public List<Error> ValidateRecord(Dictionary<string, object> record)
        {
            var values = new Dictionary<string, string>();
            return ValidateRecord(record, out values);
        }

        /// <summary>
        /// Validates a record.
        /// </summary>
        /// <param name="record">The record to validate.</param>
        /// <param name="values">The validated record. This dictionary might not contain a valid record. It's only valid if the return list is empty.</param>
        /// <returns>Returns a list of errors if the record is not valid; otherwise an empty list.</returns>
        public List<Error> ValidateRecord(IDataRecord record, out Dictionary<string, string> values)
        {
            var dict = GetDict(record);
            return ValidateRecord(dict, out values);
        }

        /// <summary>
        /// Validates a record.
        /// </summary>
        /// <param name="record">The record to validate.</param>
        /// <param name="values">The validated record. This dictionary might not contain a valid record. It's only valid if the return list is empty.</param>
        /// <returns>Returns a list of errors if the record is not valid; otherwise an empty list.</returns>
        public List<Error> ValidateRecord(Dictionary<string, object> record, out Dictionary<string, string> values)
        {
            values = new Dictionary<string, string>();
            var errors = new List<Error>();
            var seriesKey = new Dictionary<string, string>();

            foreach (var dim in KeyFamily.Dimensions)
            {
                ValidateComponent(dim, record, values, errors, "Dimension");

                // build series key
                string name = dim.Concept.Id;
                string value = null;
                if (values.TryGetValue(name, out value))
                {
                    seriesKey.Add(name, value);
                }
            }

            foreach (var attr in KeyFamily.Attributes)
            {
                ValidateComponent(attr, record, values, errors, "Attribute");
            }

            if (KeyFamily.TimeDimension != null)
            {
                ValidateComponent(KeyFamily.TimeDimension, record, values, errors, "Time dimension");
                string name = KeyFamily.TimeDimension.Concept.Id;
                string value = null;
                if (values.TryGetValue(name, out value))
                {
                    seriesKey.Add(name, value);
                }
            }

            if (KeyFamily.PrimaryMeasure != null)
            {
                ValidateComponent(KeyFamily.PrimaryMeasure, record, values, errors, "Primary measure");
            }

            string seriesKeyString = ValuesToString(seriesKey);
            if (_keys.ContainsKey(seriesKeyString))
            {
                var error = new DuplicateKeyError("Duplicate key: {0}", seriesKeyString);
                errors.Add(error);
            }
            else
            {
                _keys.Add(seriesKeyString, true);
            }

            return errors;
        }

        string ValuesToString(Dictionary<string, string> record)
        {
            var list = new List<string>();

            foreach (var item in record)
            {
                list.Add(string.Format("{0}={1}", item.Key, item.Value));
            }

            return string.Join(",", list.ToArray());
        }

        void ValidateComponent(Component component, Dictionary<string, object> record, Dictionary<string, string> values, List<Error> errors, string componentName)
        {
            string name = component.Concept.Id;
            object obj = null;
            bool optionalAttribute = component is Attribute && ((Attribute)component).AssignmentStatus == AssignmentStatus.Conditional;
            if (!record.TryGetValue(name, out obj))
            {   
                if (!optionalAttribute)
                {
                    errors.Add(new MandatoryComponentMissing("{0} '{1}' is missing from record ({2}).", componentName, name, RecordToString(record)));
                }

                values.Add(name, null);
            }
            else
            {
                if (obj == null || obj is DBNull)
                {
                    if (!optionalAttribute)
                    {
                        errors.Add(new MandatoryComponentMissing("Null value for {0}: Name:'{1}' Value:'null' Record ({2}).", componentName, name, RecordToString(record)));
                    }

                    values.Add(name, null);
                }
                else
                {
                    string value = null;
                    string startTime = null;
                    if (!component.TrySerialize(obj, out value, out startTime))
                    {
                        errors.Add(new SerializationError("Cannot serialize {0}: Name:'{1}' Value:'{2}' Type:'{4}' Record ({3}).", componentName, name, obj, RecordToString(record), obj.GetType()));
                    }
                    else
                    {
                        values.Add(name, value);
                    }
                }
            }
        }

        Dictionary<string, object> GetDict(IDataRecord record)
        {
            var dict = new Dictionary<string, object>();

            for (int i = 0; i < record.FieldCount; i++)
            {
                dict.Add(record.GetName(i), record[i]);
            }
            return dict;
        }

        string RecordToString(Dictionary<string, object> record)
        {
            var list = new List<string>();
            record.ForEach(i => list.Add(string.Format("{0}={1}", i.Key, i.Value)));
            return string.Join(",", list.ToArray());
        }
    }
}
