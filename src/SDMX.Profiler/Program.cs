using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SDMX.Tests;

namespace SDMX.Profiler
{
    class Program
    {
        static void Main(string[] args)
        {            
            // new DataReaderTests().LoadTest();
            new StructureTests().LoadTest();
        }

        private static Header GetHeader()
        {
            return new Header((Id)"MSD_HDR", new Party((Id)"UIS"))
            {
                Prepared = DateTime.Now
            };
        }

        static string GetPathFromProjectBase(string path)
        {
            return "..\\..\\..\\..\\" + path;
        }
    }
}
