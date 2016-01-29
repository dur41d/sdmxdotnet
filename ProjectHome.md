## What is SDMX.NET? ##

SDMX is a large and complex standard which makes creating SDMX-capable applications a difficult and error-prone activity. Using an SDMX specific library, which is an accurate implementation of the standard, will enable developers to create robust and production ready SDMX applications. **SDMX.NET** is that library.‬

  * Completely open source.
  * Produce and consume SDMX from any data source (like Excel or Database).
  * Accurate implementation of the supported SDMX artefacts.
  * Written in C# and thus compatible with any .NET application.
  * Intuitive and easy to use API.
  * Optimized for scalability and performance.

## Examples ##
To give an idea of what the framework can accomplish, here is a code sample in **C#** for inserting an SDMX file into the database. It is important to note that the library ensures that the resulting dataset is 100% valid based on the key family definition.
```
// load the structure definition
var structure = StructureMessage.Load("StructureSample.xml");
var keyFamily = structure.KeyFamilies[0];

// create IDataReader from the data file
using (var reader = DataReader.Create("GenericSample.xml", keyFamily))
{
    // use bulk insert into the database
    using (var bulkCopy = new SqlBulkCopy("connection string"))
    {                   
         bulkCopy.DestinationTableName = "TargetTableName";
         bulkCopy.WriteToServer(reader);
    }
}
```

The `DataReader` created implements the `IDataReader` interface and thus can be used like an ADO.NET `DataReader` that is familiar to .NET programmers.

The following example shows how to create a new code list and save it to a file.

```
var codelist = new CodeList(new InternationalString("en", "Countries"), "CL_COUNTRY", "UIS");

var code = new Code("CAN");
code.Description["en"] = "Canada";
codelist.Add(code);

code = new Code("USA");
code.Description["en"] = "United States of America";
codelist.Add(code);

var message = new StructureMessage();
message.Header = new Header("MSD_HDR", new Party("UIS")) { Prepared = DateTime.Now };            
message.CodeLists.Add(codelist);
message.Save("CL_COUNTRY.xml");
```

Of course, the framework has many other capabilities like creating other SDMX artefacts and converting between the different data formats.

Please feel free to download the code, modify it, and submit feature requests and suggestions.