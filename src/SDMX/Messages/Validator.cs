using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace SDMX
{
    public class MessageValidator
    {
        private static XmlSchemaSet _schemas;

        public static bool ValidateXml(string fileName, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(fileName), warning, error);
        }
        
        public static bool ValidateXml(Stream stream, Action<string> warning, Action<string> error)
        {
            return ValidateXml(XmlReader.Create(stream), warning, error);
        }

        public static bool ValidateXml(XmlReader reader, Action<string> warning, Action<string> error)
        {
            bool isValid = true;

            var schemas = GetSchemas();

            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(schemas);
            settings.ValidationEventHandler += (s, args) =>
            {
                isValid = false;
                if (warning != null && args.Severity == XmlSeverityType.Warning)
                    warning(args.Message);
                else if (error != null)
                    error(args.Message);
            };

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
