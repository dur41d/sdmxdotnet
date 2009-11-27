using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OXM;
using Common;
using System.Xml.Linq;

namespace SDMX.Parsers
{
    internal class CodeListMap : MaintainableArtefactMap<CodeList>
    {
        CodeList _codeList;
        
        public CodeListMap()
        {
            AttributesOrder("id", "agencyID", "version", "uri", "isExternalReference", "isFinal", "validFrom", "validTo");
            ElementsOrder("Name", "Description", "Code", "Annotations");

            MapCollection(o => o).ToElement("Code", false)
                .Set(v => _codeList.Add(v))
                .ClassMap(() => new CodeMap(_codeList));
        }

        ID _id;
        ID _agencyID;
        bool _isFinal;
        string _version;
        ITimePeriod _validTo;
        ITimePeriod _validFrom;
        Uri _uri;
        bool _isExternalReference;

        protected override void SetAgencyID(ID agencyId)
        {
            _agencyID = agencyId;
        }

        protected override void SetID(ID id)
        {
            _id = id;
        }

        protected override void SetIsFinal(bool isFinal)
        {
            _isFinal = isFinal;
        }

        protected override void SetIsExternalReference(bool isExternalReference)
        {
            _isExternalReference = isExternalReference;
        }

        protected override void SetVersion(string version)
        {
            _version = version;
        }

        protected override void SetValidTo(ITimePeriod validTo)
        {
            _validTo = validTo;
        }

        protected override void SetValidFrom(ITimePeriod validFrom)
        {
            _validFrom = validFrom;
        }

        protected override void SetUri(Uri uri)
        {
            _uri = uri;
        }

        protected override void SetName(InternationalString name)
        {
            if (_codeList == null)
            {
                _codeList = new CodeList(name, _id, _agencyID);
                _codeList.IsFinal = _isFinal;
                _codeList.Version = _version;
                _codeList.ValidFrom = _validFrom;
                _codeList.ValidTo = _validTo;
                _codeList.Uri = _uri;
                _codeList.IsExternalReference = _isExternalReference;
            }
            else
            {
                _codeList.Name.Add(name);
            }
        }

        protected override void SetDescription(InternationalString description)
        {
            _codeList.Description.Add(description);
        }

        protected override void AddAnnotation(Annotation annotation)
        {
            _codeList.Annotations.Add(annotation);
        }

        protected override CodeList Return()
        {
            return _codeList;
        }
    }
}
