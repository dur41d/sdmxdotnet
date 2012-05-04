using System;
using Common;
using System.Data;
using System.Collections.Generic;
using System.Linq;

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

        public List<Error> Validate(IDataReader reader)
        {
            var list = new List<Error>();
            while (reader.Read())
            {
                list.AddRange(ValidateRecord(reader));
            }
            return list;
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
                ValidateComponent(dim, record, values, errors, "Dimension", message => new ValidationError(message));

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
                ValidateComponent(attr, record, values, errors, "Attribute", message => new ValidationError(message));
            }

            if (KeyFamily.TimeDimension != null)
            {
                ValidateComponent(KeyFamily.TimeDimension, record, values, errors, "Time dimension", message => new ValidationError(message));
                string name = KeyFamily.TimeDimension.Concept.Id;
                string value = null;
                if (values.TryGetValue(name, out value))
                {
                    seriesKey.Add(name, value);
                }
            }

            if (KeyFamily.PrimaryMeasure != null)
            {
                ValidateComponent(KeyFamily.PrimaryMeasure, record, values, errors, "Primary measure", message => new ValidationError(message));
            }

            string seriesKeyString = ValuesToString(seriesKey);
            if (_keys.ContainsKey(seriesKeyString))
            {
                var error = new ValidationError("Duplicate key: {0}", seriesKeyString);
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

        void ValidateComponent(Component component, Dictionary<string, object> record, Dictionary<string, string> values, List<Error> errors, string componentName, Func<string, Error> getError)
        {
            string name = component.Concept.Id;
            object obj = null;
            bool optionalAttribute = component is Attribute && ((Attribute)component).AssignmentStatus == AssignmentStatus.Conditional;
            if (!record.TryGetValue(name, out obj))
            {   
                if (!optionalAttribute)
                {
                    errors.Add(getError(string.Format("{0} '{1}' is missing from record ({2}).", componentName, name, RecordToString(record))));
                }

                values.Add(name, null);
            }
            else
            {
                if (obj == null || obj is DBNull)
                {
                    if (!optionalAttribute)
                    {
                        errors.Add(new ValidationError("Null value for {0}: Name:'{1}' Value:'null' Record ({2}).", componentName, name, RecordToString(record)));
                    }

                    values.Add(name, null);
                }
                else
                {
                    string value = null;
                    string startTime = null;
                    if (!component.TrySerialize(obj, out value, out startTime))
                    {
                        errors.Add(new ValidationError("Cannot serialize {0}: Name:'{1}' Value:'{2}' Record ({3}).", componentName, name, value, RecordToString(record)));
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
            record.ForEach(i => list.Add(string.Format("{0}={1}", i.Key, i.Value.ToString())));
            return string.Join(",", list.ToArray());
        }
    }
}
