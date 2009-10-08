using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;

namespace SDMX.Parsers
{
    public class KeyFamilyMap : MaintainableArtefactMap<KeyFamily>
    {
        private DSD _dsd;        

        public KeyFamilyMap(DSD dsd)
        {
            _dsd = dsd;
            
            MapElementContainer("Components", false)
                .MapElementCollection<Dimension>("Dimension", false)
                .Parser(new DimensionMap(dsd))
                .Getter(o => o.Dimensions)
                .Setter((o, list) => list.ForEach(item => o.AddDimension(item)));
        }

        protected override KeyFamily CreateObject()
        {
            return new KeyFamily(_IdMap.Value, _agencyIDMap.Value);
        }
    }
}
