using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SDMX.Parsers
{  
    internal class KeyFamilyParser
    {   
        internal KeyFamily Parse(DSDDocument dsd)
        {
            KeyFamily keyFamily = new KeyFamily();

            XElement kfElement = dsd.GetKeyFamilyElement();

            foreach (var keFamilyElement in kfElement.Elements())
            {
                if (keFamilyElement.Name.LocalName == "Components")
                {
                    foreach (var element in keFamilyElement.Elements())
                    {
                        if (element.Name.LocalName == "Dimension")
                        {
                            var parser = new DimensionParser();
                            var dimension = parser.Parse(element, dsd);
                            keyFamily.AddDimension(dimension);
                        }
                    }
                }
            }

            return keyFamily;

        }

        private class DimensionParser
        {
            internal Dimension Parse(XElement element, DSDDocument dsd)
            {
                string conceptRef, conceptVersion, conceptAgency, conceptSchemeRef, conceptSchemeAgency;
                ParseAttributes(element, out conceptRef, out conceptVersion, out conceptAgency, out conceptSchemeRef, out conceptSchemeAgency);

                return null;

            }

            private void ParseAttributes(XElement element, out string conceptRef, out string conceptVersion, 
                out string conceptAgency, out string conceptSchemeRef, out string conceptSchemeAgency)
            {
                conceptRef = element.Attribute("conceptRef").Value;
                var attribute = element.Attribute("conceptVersion");
                conceptVersion = null;
                if (attribute != null)
                {
                    conceptVersion = attribute.Value;   
                }

                attribute = element.Attribute("conceptAgency");
                conceptAgency = null;
                if (attribute != null)
                {
                    conceptAgency = attribute.Value;
                }

                attribute = element.Attribute("conceptSchemeRef");
                conceptSchemeRef = null;
                if (attribute != null)
                {
                    conceptSchemeRef = attribute.Value;
                }

                attribute = element.Attribute("conceptSchemeAgency");
                conceptSchemeAgency = null;
                if (attribute != null)
                {
                    conceptSchemeAgency = attribute.Value;
                }
            }
        }
    }

    internal class DSDDocument
    {
        internal DSDDocument(XDocument dsd)
        { 
            
        }

        internal XElement GetKeyFamilyElement()
        {
            return null;
        }
    }
}
