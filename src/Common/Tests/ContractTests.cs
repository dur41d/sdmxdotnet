using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common;

namespace SDMX.Tests
{
    //public class Address
    //{
    //    public string Street {get; set;}
    //    public string City {get; set;}
    //}
    
    //public class Person
    //{
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public Address HomeAddress { get; set; }
    //}

    //public class HomeAddressMap
    //{ 
        
    //}


    //public class PersionMap
    //{
    //    public PersionMap()
    //    {
    //        Map(o => o.FirstName).ToAttribute("firstName", true).Set(p => Instance.FristName = p);
    //        Map(o => o.HomeAdress).ToAttributes().Using(new HomeAddressMap());
    //    }
    //}

    /// <summary>
    /// <Person firstName="duraid" lastName="Abbas" street="11 street" city="Montreal" />
    /// </summary>
    
    [TestFixture]
    public class ContractTests
    {
        [Test]
        public void AssertNotNull()
        {
            StringBuilder o = new StringBuilder("some object");

            Contract.AssertNotNull(() => o);

            o = null;

            Assert.Throws<ArgumentNullException>(
                    () => Contract.AssertNotNull(() => o)
                );

            o = new StringBuilder("other object");

            Contract.AssertNotNull(() => o);
        }

        [Test]
        public void AssrtNotNullOrEmpty()
        {
            string o = "   ";

            Assert.Throws<ArgumentException>(
                    () => Contract.AssertNotNullOrEmpty(() => o)
                );
            o = "some string";

            Contract.AssertNotNullOrEmpty(() => o);
        }    
    }
}
