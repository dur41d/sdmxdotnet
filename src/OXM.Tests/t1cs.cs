using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;
using System.Xml;

namespace OXM.Tests
{


    public abstract class IOCommand
    {
    }

    public class ExcelExportCommand : IOCommand
    {
    }

    public class SdmxImportCommand : IOCommand
    {
    }

    public class Batch
    {
        public string Name { get; set; }
        public List<IOCommand> Commands { get; private set; }

        public Batch()
        {
            Commands = new List<IOCommand>();
        }
    }

    public class BatchProxy
    { 
        public string Name { get; set; }
        public List<ExcelExportCommand> ExcelExportCommands { get; private set; }
        public List<SdmxImportCommand> SdmxImportCommands { get; private set; }

        public BatchProxy()
        {
            ExcelExportCommands = new List<ExcelExportCommand>();
        }
    }

    public class ExcelExportCommandMap : ClassMap<ExcelExportCommand>
    {
        public ExcelExportCommandMap()
        { 
            
        }

        protected override ExcelExportCommand Return()
        {
            return new ExcelExportCommand();
        }
    }

    public class SdmxImportCommandMap : ClassMap<SdmxImportCommand>
    {
        public SdmxImportCommandMap()
        {

        }

        protected override SdmxImportCommand Return()
        {
            return new SdmxImportCommand();
        }
    }

    public class BatchMap : RootElementMap<Batch>
    {
        Batch _batch = new Batch();

        public BatchMap()
        {
            Map(o => o.Name).ToAttribute("name", false)
                .Set(v => _batch.Name = v)
                .Converter(new StringConverter());

            MapCollection(m => m.Commands.OfType<ExcelExportCommand>())
                    .ToElement("ExcelExportCommand", false)
                    .Set(cmd => _batch.Commands.Add(cmd))
                    .ClassMap(() => new ExcelExportCommandMap());

            MapCollection(m => m.Commands.OfType<SdmxImportCommand>())
                    .ToElement("SdmxImportCommand", false)
                    .Set(cmd => _batch.Commands.Add(cmd))
                    .ClassMap(() => new SdmxImportCommandMap());

        }

        protected override Batch Return()
        {
            return _batch;
        }

        public override XName Name
        {
            get { return "Batch"; }
        }
    }

     
    public class t1
    {
        public List<string> t2 { get; private set; }
        public List<string> t3 { get; private set; }

        public t1()
        {
            t2 = new List<string>();
            t3 = new List<string>();
        }
    }

    public class t1Map : RootElementMap<t1>
    {
        t1 _t1 = new t1();

        public t1Map()
        {
            var container = MapContainer("container", true);

            container.MapCollection(o => o.t2).ToSimpleElement("t2", false)
                .Set(v => _t1.t2.Add(v))
                .Converter(new StringConverter());


            container.MapCollection(o => o.t2).ToSimpleElement("t3", false)
                .Set(v => _t1.t3.Add(v))
                .Converter(new StringConverter());

        }

        protected override t1 Return()
        {
            return _t1;
        }

        public override XName Name
        {
            get { return "t1"; }
        }
    }

    [TestFixture]
    public class t1Tests
    {
        [Test]
        public void t1_test()
        {
            var doc = XDocument.Parse(
@"<t1>
    <container>
        <t2>1</t2>
        <t2>2</t2>
        <t2>3</t2>
        <t3>1</t3>
        <t3>2</t3>
    </container>
</t1>");
            var map = new t1Map();

            t1 _t1;
            using (var reader = doc.CreateReader())
            {
                _t1 = map.ReadXml(reader);
            }

            Assert.AreEqual(3, _t1.t2.Count);
            Assert.AreEqual(2, _t1.t3.Count);
        }

        [Test]
        public void BatchMap()
        {
            var batch = new Batch();
            batch.Commands.Add(new SdmxImportCommand());
            batch.Commands.Add(new SdmxImportCommand());
            batch.Commands.Add(new ExcelExportCommand());

            var batchMap = new BatchMap();

            using (var writer = XmlWriter.Create(Console.Out, new XmlWriterSettings() { Indent = true }))
            {
                batchMap.WriteXml(writer, batch);
            }

        }
    }
}
