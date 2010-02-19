using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SDMX.Profiler
{
    class Program
    {
        static void Main(string[] args)
        {            
            string dsdPath = GetPathFromProjectBase("lib\\StructureSample.xml");
            var dsd = StructureMessage.Load(dsdPath);
            var keyFamily = dsd.KeyFamilies[0];
            var dataSet = new DataSet(keyFamily);
                     
            //Console.Write("Enter to start");
            //Console.ReadLine();

            foreach (var freq in keyFamily.Dimensions.Get("FREQ").CodeList)
            {
                foreach (var jdtype in keyFamily.Dimensions.Get("JD_TYPE").CodeList)
                {
                    foreach (var jdcat in keyFamily.Dimensions.Get("JD_CATEGORY").CodeList)
                    {
                        foreach (var city in keyFamily.Dimensions.Get("VIS_CTY").CodeList)
                        {
                            var key = dataSet.NewKey();
                            key["FREQ"] = freq;
                            key["JD_TYPE"] = jdtype;
                            key["JD_CATEGORY"] = jdcat;
                            key["VIS_CTY"] = city;
                            var timer1 = DateTime.Now;
                            var series = dataSet.Series.Create(key);
                            series.Attributes["TIME_FORMAT"] = "P1Y";
                            series.Attributes["COLLECTION"] = "A";

                            for (int i = 1959; i < 2009; i++)
                            {
                                var obs = series.Create(new YearTimePeriod(i));

                                obs.Value = new DecimalValue(3.3m);
                                obs.Attributes["OBS_STATUS"] = "A";

                                timer1 = DateTime.Now;
                                series.Add(obs);                                
                            }

                            dataSet.Series.Add(series);
                        }
                    }
                }
            }

            //Console.Write("Enter to quit");
            //Console.ReadLine();
        }

        static string GetPathFromProjectBase(string path)
        {
            return "..\\..\\..\\..\\" + path;
        }
    }
}
