using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    internal class GroupValuesHolder : AnnotableArtefact
    {
        public ID ID { get; set; }
        public List<ID> DimensionRefs { get; set; }
        public ID AttachmentConstraintRef { get; set; }
        public InternationalText Description { get; private set; }

        public GroupValuesHolder()
        {
            DimensionRefs = new List<ID>();
            Description = new InternationalText();
        }
    }

    internal class GroupMap : AnnotableArtefactMap<GroupValuesHolder>
    {
        AttributeMap<GroupValuesHolder, ID> _id;        

        public GroupMap()
        {
            _id = MapAttribute<ID>("id", true)
                .Getter(o => o.ID)
                .Setter(p => Instance.ID = p)
                .Parser(s => new ID(s));

            MapElementCollection<ID>("DimensionRef", true)
                .Getter(o => o.DimensionRefs)
                .Parser(() => new ValueElementMap<ID>(s => new ID(s)))
                .Setter(p => Instance.DimensionRefs.Add(p));

            MapElement<ID>("AttachmentConstraintRef", false)
                .Getter(o => o.AttachmentConstraintRef)
                .Setter(p => Instance.AttachmentConstraintRef = p)
                .Parser(new ValueElementMap<ID>(s => new ID(s)));

            MapElementCollection<KeyValuePair<Language, string>>("Description", false)
                .Getter(o =>
                {
                    var list = new List<KeyValuePair<Language, string>>();
                    foreach (var lang in o.Description.Languages)
                    {
                        var item = new KeyValuePair<Language, string>(lang, o.Description[lang]);
                        list.Add(item);
                    }
                    return list;
                })
                .Setter(p => Instance.Description[p.Key] = p.Value)
                .Parser(() => new InternationalStringMap());    
        }

        //protected override GroupValuesHolder CreateObject()
        //{
        //    return new GroupValuesHolder();
            
        //}
    }
}
