SDMX Technical Standards Version 2.0
____________________________________

This ZIP file contains SDMX schemas which are designed for general use as part of the standard,
as well as several sample schemas and XML instances which are intended to be illustrative of how 
SDMX is used.

All files should be unzipped into a single local directory before use.

The following list explains which files are for general use, and which are samples.


SDMX Schemas
_____________

SDMXMessage.xsd (Ties all other general purpose schemas together and provides a common envelope and header)
SDMXStructure.xsd
SDMXGenericData.xsd
SDMXCommon.xsd
xml.xsd (standard W3C schema used by SDMXCommon.xsd, and included for convenience)
SDMXQuery.xsd
SDMXGenericMetadata.xsd
SDMXRegistry.xsd
SDMXUtilityData.xsd
SDMXCompactData.xsd
SDMXCrossSectionalData.xsd
SDMXMetadataReport.xsd

Samples of Generated Schemas
____________________________

These schemas are specific to key families or metadata structure definitions. Each is derived from a sample 
structure file, and each has an accompanying data sample, as indicated.

BIS_JOINT_DEBT_Compact.xsd (derived from StructureSample.xml, accompanied by CompactSample.xml)
BIS_JOINT_DEBT_Utility.xsd (derived from StructureSample.xml, accompanied by UtilitySample.xml)
BIS_JOINT_DEBT_CrossSectional.xsd (derived from StructureSample.xml, accompanied by CrossSectionalSample.xml)
IMF_CONTACT_MetadataReport.xsd (derived from ContactMDStructureSample.xml, accompanied by MetadataReportSample.xml)

Sample XML Instances
_____________________

As mentioned in section above:

StructureSample.xml (key family example)
CompactSample.xml
UtilitySample.xml
CrossSectionalSample.xml
ContactMDStructureSample.xml
MetadataReportSample.xml

Additional sample files:

GenericSample.xml (agrees with StructureSample.xml)
GenericMetadataSample.xml (agrees with ContactMDStructureSample.xml)
GenericMetadataSample2.xml (agrees with ContactMDStructureSample.xml)
QuerySample.xml

Registry Interface Samples:

With the exception of NotifyRegistryEvent.xml, which has no request-response pattern, all of these samples are in pairs. 
The request sample corresponds with the response sample.

NotifyRegistryEvent.xml

QueryProvisioningRequest.xml
QueryProvisioningResponse.xml
SubmitProvisioningRequest.xml
SubmitProvisioningResponse.xml
QueryStructureRequest.xml
QueryStructureResponse.xml
SubmitStructureRequest.xml
SubmitStructureResponse.xml
SubmitRegistrationRequest.xml
SubmitRegistrationResponse.xml
QueryRegistrationRequest.xml
QueryRegistrationResponse.xml
SubmitSubscriptionRequest.xml
SubmitSubscriptionResponse.xml


