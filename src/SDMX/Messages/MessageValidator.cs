using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections.Generic;

namespace SDMX
{
    public class MessageValidator
    {
        private static XmlSchemaSet _schemas;

        private static List<string> _customSchemas = new List<string>();

        public static bool ValidateXml(string fileName)
        {
            return ValidateXml(XmlReader.Create(fileName), null, null, null);
        }
        
        public static bool ValidateXml(Stream stream)
        {
            return ValidateXml(XmlReader.Create(stream), null, null, null);
        }

        public static bool ValidateXml(XmlReader reader)
        {
            return ValidateXml(reader, null, null, null);
        }

        public static bool ValidateXml(string fileName, string customSchema)
        {
            return ValidateXml(XmlReader.Create(fileName), customSchema, null, null);
        }

        public static bool ValidateXml(Stream stream, string customSchema)
        {
            return ValidateXml(XmlReader.Create(stream), customSchema, null, null);
        }

        public static bool ValidateXml(XmlReader reader, string customSchema)
        {
            return ValidateXml(reader, customSchema, null, null);
        }

        public static bool ValidateXml(string fileName, string customSchema, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(fileName), customSchema, warning, error);
        }

        public static bool ValidateXml(Stream stream, string customSchema, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(stream), customSchema, warning, error);
        }

        public static bool ValidateXml(string fileName, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(fileName), null, warning, error);
        }
        
        public static bool ValidateXml(Stream stream, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(stream), null, warning, error);
        }

        public static bool ValidateXml(XmlReader reader, Action<string> warning, Action<string> error)
        {
            return ValidateXml(reader, null, warning, error);
        }

        public static bool ValidateXml(XmlReader reader, string customSchema, Action<string> warning, Action<string> error)
        {
            bool isValid = true;

            var schemas = GetSchemas();

            if (customSchema != null)
            {
                schemas.Add(null, XmlReader.Create(customSchema));
            }

            ValidationEventHandler handler = (s, args) =>
            {
                isValid = false;
                if (warning != null && args.Severity == XmlSeverityType.Warning)
                    warning(args.Message);
                else if (error != null)
                    error(args.Message);
            };

            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(schemas);
            settings.ValidationEventHandler += handler;

            var r = XmlReader.Create(reader, settings);
            while (r.Read()) ;

            return isValid;
        }

        private static XmlSchemaSet GetSchemas()
        {
            if (_schemas == null)
            {
                _schemas = new XmlSchemaSet();

                _schemas.Add(ReadSchema("xml.xsd"));
                _schemas.Add(ReadSchema("SDMXCommon.xsd"));
                _schemas.Add(ReadSchema("SDMXCompactData.xsd"));
                _schemas.Add(ReadSchema("SDMXCrossSectionalData.xsd"));
                _schemas.Add(ReadSchema("SDMXGenericData.xsd"));
                _schemas.Add(ReadSchema("SDMXGenericMetadata.xsd"));
                _schemas.Add(ReadSchema("SDMXMetadataReport.xsd"));
                _schemas.Add(ReadSchema("SDMXQuery.xsd"));
                _schemas.Add(ReadSchema("SDMXRegistry.xsd"));
                _schemas.Add(ReadSchema("SDMXStructure.xsd"));
                _schemas.Add(ReadSchema("SDMXUtilityData.xsd"));
                _schemas.Add(ReadSchema("SDMXMessage.xsd"));
            }

            return _schemas;
        }

        private static XmlSchema ReadSchema(string schemaName)
        {
            using (var s = typeof(IMessage).Assembly.GetManifestResourceStream("SDMX.Schemas." + schemaName))
            {
                return XmlSchema.Read(s, null);
            }
        }
    }
}
