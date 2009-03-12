using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SDMX_ML.Framework.Interfaces;
using SDMX_Common = SDMX_ML.Framework.Common;
using SDMX_Message = SDMX_ML.Framework.Message;

namespace SDMX_ML.Framework.Message
{
    public class HeaderType
    {
        public enum EnumDataSetAction{
            Append,
            Replace,
            Delete,
            Information
            }
        private XNamespace _nsQueryMessage;
        private XNamespace _nsQuery;
        private XNamespace _default;
        private XElement _xml;

        private PartyType _sender = new PartyType();
        private PartyType _receiver = new PartyType();
        private string _id;
        private bool _test;
        private bool _truncated;
        private string  _name;
        private EnumDataSetAction _datasetaction;
        private string _extracted;
        private string _prepared;
        private string _datasetagency;
        private string _datasetid;

        public HeaderType()
        {
        }

        public HeaderType(XElement header)
        {
            _xml = header;
            SetXMLHeader();

        }


        #region Properties

        public PartyType Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public PartyType Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Test
        {
            get { return _test; }
            set { _test = value; }
        }

        public bool Truncated
        {
            get { return _truncated; }
            set { _truncated = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Prepared
        {
            get { return _prepared; }
            set { _prepared = value; }
        }

        public string DataSetAgency
        {
            get { return _datasetagency; }
            set { _datasetagency = value; }
        }

        public string DataSetID
        {
            get { return _datasetid; }
            set { _datasetid = value; }
        }

        public EnumDataSetAction DataSetAction
        {
            get { return _datasetaction; }
            set { _datasetaction = value; }
        }

        public string Extracted
        {
            get { return _extracted; }
            set { _extracted = value; }
        }

        #endregion

        #region WriteXML

        public string ToXml()
        {
            XElement header = GetXml();

            return header.ToString();
        }

        public XElement GetXml()
        {
            XElement header = new XElement(Namespaces.GetNS("message") + "Header", 
                new XElement(Namespaces.GetNS("message") + "ID", _id),
                new XElement(Namespaces.GetNS("message") + "Test", _test),
                new XElement(Namespaces.GetNS("message") + "Truncated", _truncated.ToString().ToLower()),
                new XElement(Namespaces.GetNS("message") + "Name", 
                    _name, new XAttribute(XNamespace.Xml + "lang", "en")),
                new XElement(Namespaces.GetNS("message") + "Prepared", _prepared));
                
             XElement sender = new XElement(Namespaces.GetNS("message") + "Sender", 
                 new XAttribute("id", "DN"),
                 new XElement(Namespaces.GetNS("message") + "Name", "Danmarks Nationalbank", 
                    new XAttribute(XNamespace.Xml +"lang", "da")));

            XElement receiver = new XElement(Namespaces.GetNS("message") + "Receiver", new XAttribute("id", _receiver.Id),
                    new XElement(Namespaces.GetNS("message") + "Name", _receiver.Name.Text, new XAttribute(XNamespace.Xml + "lang", _receiver.Name.Lang)));
             
            header.Add(GetContact(sender, _sender.Contact));
            header.Add(GetContact(receiver, _receiver.Contact));

            header.Add(new XElement(Namespaces.GetNS("message") + "DataSetAction", _datasetaction));
            header.Add(new XElement(Namespaces.GetNS("message") + "Extracted", _extracted));

            return header;

        }

        
        private XElement GetContact(XElement sr, List<ContactType> contacts)
        {
 
            foreach(ContactType c in contacts)
            {
                XElement contact = 
                    new XElement(Namespaces.GetNS("message") + "Contact");
                
                if(c.Name != null)
                {
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Name", c.Name.Text, 
                        new XAttribute(XNamespace.Xml + "lang", c.Name.Lang)));
                }

                if(c.Department != null)
                {
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Department", c.Department.Text, 
                        new XAttribute(XNamespace.Xml + "lang", c.Name.Lang)));
                }
                    
                if(c.Telephone != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Telephone", c.Telephone));

                if(c.Fax != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Fax", c.Fax));
                
                if(c.X400 != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "X400", c.X400));
                
                if(c.Uri != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Uri", c.Uri));
                    
                if(c.Role != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Role", c.Role));

                if(c.Email != null)
                    contact.Add(new XElement(Namespaces.GetNS("message") + "Email", c.Email));

               

                sr.Add(contact);
            }

            return sr;
        }

        #endregion

        #region CreateHeader

        private void SetXMLHeader()
        {
 
            _default = Namespaces.GetNS("message");

            XElement header = _xml.Element(_default + "Header");

            if(header.Element(_default + "ID") != null)
                _id = header.Element(_default + "ID").Value;
            if(header.Element(_default + "Test") != null)
                _test = Convert.ToBoolean(header.Element(_default + "Test").Value);
            if(header.Element(_default + "Truncated") != null)
                _truncated = Convert.ToBoolean(header.Element(_default + "Truncated").Value);
            if(header.Element(_default + "Name") != null)
                _name = header.Element(_default + "Name").Value;
            if(header.Element(_default + "Prepared") != null)
                _prepared = header.Element(_default + "Prepared").Value;
            
            if(header.Element(_default + "Sender") != null)
                _sender = GetParty(header.Element(_default + "Sender"));

            if(header.Element(_default + "Receiver") != null)
                _receiver = GetParty(header.Element(_default + "Receiver"));
        }

        private SDMX_Message.PartyType GetParty(XElement party)
        {
            SDMX_Message.PartyType element = new SDMX_Message.PartyType();

            if(party.Attribute("id") != null)
                element.Id = party.Attribute("id").Value;

            if(party.Element(_default + "Name") != null)
            {
                XElement name = party.Element(_default + "Name");
                
                element.Name = GetTextType(name);
            }

            foreach(XElement c in party.Elements(_default + "Contact"))
                element.Contact.Add(GetContact(c));

            return element;

        }

        private SDMX_Message.ContactType GetContact(XElement contact)
        {
            SDMX_Message.ContactType con = new SDMX_Message.ContactType();

            XElement name = contact.Element(_default + "Name");

            con.Name = GetTextType(contact.Element(_default + "Name"));

            if(contact.Element(_default + "Telephone") != null)
                con.Telephone = contact.Element(_default + "Telephone").Value;

            if(contact.Element(_default + "Department") != null)
                con.Department = GetTextType(contact.Element(_default + "Department"));

            if(contact.Element(_default + "Role") != null)
                con.Role = GetTextType(contact.Element(_default + "Role"));


            return con;

        }

        private SDMX_Common.TextType GetTextType(XElement type)
        {
            SDMX_Common.TextType text = new SDMX_Common.TextType();

            text.Text = type.Value;

            if(type.Attribute(XNamespace.Xml + "lang") != null)
                text.Lang = type.Attribute(XNamespace.Xml + "lang").Value;


            return text;

        }


        #endregion
    }
}
