using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace SDMX.Tests.NewModel
{
    //internal class ElementValidator
    //{
    //    private Dictionary<string, int> elementCounts = new Dictionary<string, int>();
    //    private List<ElementContract> contracts = new List<ElementContract>();

    //    internal void AddName(string elementName)
    //    {
    //        AssertElementExists(elementName);
    //        int currnetCount = elementCounts.GetValueOrDefault(elementName, 0);
    //        elementCounts[elementName] = ++currnetCount;
    //    }

    //    private void AssertElementExists(string elementName)
    //    {            
    //        if (!contracts.Exists(c => c.ElementName == elementName))
    //        {
    //            throw new SDMXException("Element is not allowed '{0}'".F(elementName));
    //        }
    //    }

    //    internal void AddContract(string elementName, int minOccures, int maxOccures)
    //    {
    //        contracts.Add(new ElementContract() 
    //            { 
    //                ElementName = elementName, 
    //                MinOccures = minOccures, 
    //                MaxOccures = maxOccures 
    //            });
    //    }

    //    internal void AssertIsValid()
    //    {
    //        foreach (var contract in contracts)
    //        {
    //            int count = elementCounts.GetValueOrDefault(contract.ElementName, 0);

    //            if (contract.MinOccures > count)
    //            {
    //                throw new SDMXException("Element '{0}' has minOccures {1] but found with count '{2}'"
    //                    .F(contract.ElementName, contract.MinOccures, count));
    //            }
    //            else if (contract.MaxOccures < count)
    //            {
    //                throw new SDMXException("Element '{0}' has maxOccures {1] but found with count '{2}'"
    //                   .F(contract.ElementName, contract.MaxOccures, count));
    //            }
    //        }
    //    }

    //    private class ElementContract
    //    {
    //        public string ElementName { get; set; }
    //        public int MinOccures { get; set; }
    //        public int MaxOccures { get; set; }
    //    }
    //}
}
