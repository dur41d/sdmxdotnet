using NUnit.Framework;
using SDMX_Query = SDMX_ML.Framework.Query;
using SDMX_MESS = SDMX_ML.Framework.Messages;
using SDMX_Message = SDMX_ML.Framework.Message;
using SDMX_Common = SDMX_ML.Framework.Common;
using SDMX_Structure = SDMX_ML.Framework.Structure;
using SDMX_Generic = SDMX_ML.Framework.Generic;
using System;
using System.Xml;
using System.IO;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Linq;
using System.Reflection;

namespace SDMX.Model.Tests
{
    [TestFixture]
    public class QueryMessageTests
    {
        [Test]
        public void Can_create_QueryMessage()
        {
            //1.
            //First an object from the class QueryMessage is created.
            //The definition of a QuerMessage can be found in the QueryMessage.xsd
            SDMX_MESS.QueryMessage qm = new SDMX_MESS.QueryMessage();
            //2.
            //After this we create an object from the class Codelistwhere.
            //This is the same as the element CodelistWhere in a QueryMessage
            SDMX_Query.CodelistWhereType clwhere = new SDMX_Query.CodelistWhereType();
            //3.
            //An object is created from the class Codelist.
            //This is the same as the element in the CodelistWhere element
            SDMX_Query.CodelistType codelist = new SDMX_Query.CodelistType();
            //We set the value on what codelist we want to query
            codelist.Id = "KL_PUB_VALUTAKURS";
            //4.
            //The codelist obejct is added to the CodelistWhere object
            clwhere.Codelist = codelist;
            //5.
            //The CodelistWhere object is added to the QueryMessage
            qm.CodelistWhere.Add(clwhere);
            //6.
            //The header is created and added to the QueryMessage by calling
            //the method that return a header. The above excample
            qm.Header = GetHeader();
            //7
            //Writing the SDMX_ML QueryMeassage is now very simple because we
            //just call the method ToXml() on the QueryMessage object
            string xml = qm.ToXml();


            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(xml);
            XNamespace ns = "http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message";         
           
            

            bool isValid = ValidateQueryMessage(doc.ToString());


            Assert.IsTrue(isValid);
        }


        bool ValidateQueryMessage(string xml)
        {

            var doc = XDocument.Parse(xml);
            var schemas = new XmlSchemaSet();
            schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message", 
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\lib\\SDMXMessage.xsd"));
            
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
            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.ValidationType = ValidationType.Schema;
            //settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            //settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            //settings.Schemas.Add("http://www.SDMX.org/resources/SDMXML/schemas/v2_0/message", "Library\\SDMXMessage.xsd");


            //bool isValid = true;
            //settings.ValidationEventHandler += new ValidationEventHandler((s, args) =>
            //{
            //    isValid = false;
            //    if (args.Severity == XmlSeverityType.Warning)

            //        Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            //    else
            //        Console.WriteLine("\tValidation error: " + args.Message);
            //});

            //// Create the XmlReader object.
            //StringReader sr = new StringReader(xml);
            //XmlReader reader = XmlReader.Create(sr, settings);

            //// Parse the file. 
            //while (reader.Read()) ;
            //return isValid;
        }



        // Display any warnings or errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }

        private SDMX_Message.HeaderType GetHeader()
        {
            //Create a header object.
            SDMX_Message.HeaderType header = new SDMX_Message.HeaderType();
            //Headerid is set to the name of the KeyFamily we want to query
            header.Id = "DN_VALUTA2";
            //Set the value true telling that this message is a test meassege
            header.Test = true;
            //Set truncated to true because the data message will not
            //be truncated
            header.Truncated = false;
            //The message is given a name
            header.Name = "Some name";
            //The date we create the file is inserted
            header.Prepared = DateTime.Now.ToString("s");
            //Create an object that can hold information about the Sender
            SDMX_Message.PartyType sender = new SDMX_Message.PartyType();
            //Create an object that can hold the contact information
            SDMX_Message.ContactType sendercontact = new SDMX_Message.ContactType();
            //Create an object that can hold the name of the sender
            SDMX_Common.TextType name = new SDMX_Common.TextType();
            //Set the name of the sender
            name.Text = "Ole Sørensen";
            name.Lang = "en";
            //Add the object to the sender object
            sendercontact.Name = name;
            //Set the telephone number of the sender
            sendercontact.Telephone = "+45 99999999";
            //Add the Contact object to the sender object.
            //There can be more than one contact information
            sender.Contact.Add(sendercontact);
            //Add the sender object to the Header
            header.Sender = sender;
            //This create the receiver information the same way as
            //the sender information above
            SDMX_Message.PartyType receiver = new SDMX_Message.PartyType();
            SDMX_Message.ContactType receivercontact = new SDMX_Message.ContactType();
            SDMX_Common.TextType name2 = new SDMX_Common.TextType();
            name2.Text = "Søren Sørensen";
            receivercontact.Name = name2;
            receiver.Id = "DN";
            SDMX_Common.TextType text = new SDMX_Common.TextType();
            text.Text = "A Name";
            receiver.Name = text;
            receivercontact.Telephone = "+45 77777777";
            receiver.Contact.Add(receivercontact);
            header.Receiver = receiver;
            return header;
        }

        private void GetExchangeRates()
        {
            //1.
            //A QueryMessage is created
            SDMX_MESS.QueryMessage qm = new SDMX_MESS.QueryMessage();
            //2.
            //The DataWhere object is created
            SDMX_Query.DataWhereType dw = new SDMX_Query.DataWhereType();
            //3.
            //The And object is created. The And object is needed because
            //there is more than one value we want to use in our query.
            SDMX_Query.AndType and = new SDMX_Query.AndType();
            //4.
            //The first Dimension object is created and the id and value is set.
            //The Dimension object is added to the And object
            SDMX_Query.DimensionType kurstype = new SDMX_Query.DimensionType();
            kurstype.Id = "VAL_KURSTYPE";
            kurstype.Value = "KBH";
            and.Dimensions.Add(kurstype);
            //5.
            //The second dimension object is created and added to the And object
            SDMX_Query.DimensionType kode = new SDMX_Query.DimensionType();
            kode.Id = "VALUTA";
            kode.Value = "USD";
            and.Dimensions.Add(kode);
            //6.
            //The third dimension object is created and added to the And object
            SDMX_Query.DimensionType dim = new SDMX_Query.DimensionType();
            dim.Id = "FREQ";
            dim.Value = "D";
            and.Dimensions.Add(dim);
            //7.
            //The KeyFamily name and provider is added to the And object
            and.Keyfamily.Add("DN_VALUTA2");
            and.DataProvider.Add("DN");
            //8.
            //A PeriodType object is created because we want to query from
            //a specific date
            SDMX_Common.TimePeriodType from = new SDMX_Common.TimePeriodType();
            from.TimePeriod = "2007-11-01";
            //9.
            //A TimeType object is created and the PeriodType is added time
            SDMX_Query.TimeType time = new SDMX_Query.TimeType();
            time.StartTime = from;
            //10.
            //The TimeType is added to the And object.
            and.Time.Add(time);
            //11.
            //The and object are added to the DataWhere object
            dw.And = and;
            //12.
            //The DataWhere object are added to the QueryMessage
            qm.DataWhere.Add(dw);
            //13.
            //We get the header like the other examples
            qm.Header = GetHeader();
            //14.
            //The QueryMessage is written to a string
            string xml = qm.ToXml();
        }

        private string GenGenericData()
        {
            //1.
            //First an object from the class QueryMessage is created.
            //The definition of a QuerMessage can be found in the QueryMessage.xsd
            SDMX_MESS.QueryMessage qm = new SDMX_MESS.QueryMessage();
            //2.
            //After this an object is created from the class DataWhere.
            SDMX_Query.DataWhereType dwh = new SDMX_Query.DataWhereType();
            //3.
            //The And object that hold the dimensions to query is created
            SDMX_Query.AndType and = new SDMX_Query.AndType();
            //4.
            //Each Dimension we want to query are created.
            SDMX_Query.DimensionType d1 = new
            SDMX_Query.DimensionType("JD_CATEGORY", "A");
            SDMX_Query.DimensionType d2 = new SDMX_Query.DimensionType("FREQ", "A");
            SDMX_Query.DimensionType d3 = new SDMX_Query.DimensionType("FREQ", "M");
            //5.
            //Each dimension is added to the and object
            and.Dimensions.Add(d1);
            and.Dimensions.Add(d2);
            and.Dimensions.Add(d3);
            //4.
            //The And object is added to the DataWhere object
            dwh.And = and;
            //5.
            //The DataWhere object is added to the QueryMessage
            qm.DataWhere.Add(dwh);
            //6.
            //The header is added to the QueryMessage
            qm.Header = GetHeader();
            //7
            //Writing the SDMX_ML QueryMeassage is now wery simple because it is only
            //needed to call the method ToXml() on the QueryMessage object
            string xml = qm.ToXml();
            return xml; //Funtion return the QueryMessage as XML
        }
    }
}
