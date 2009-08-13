﻿using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SDMX.Tests
{
    public static class Utility
    {
        public static bool ValidateMessage(string xml)
        {
            var doc = XDocument.Parse(xml);
            var schemas = new XmlSchemaSet();
            schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\..\\lib\\SDMXMessage.xsd"));

            bool isValid = true;

            doc.Validate(schemas, (s, args) =>
            {
                isValid = false;
                if (args.Severity == XmlSeverityType.Warning)

                    Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
                else
                    Console.WriteLine("\tValidation error: " + args.Message);
            });

            return isValid;
        }

        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }
    }
}
