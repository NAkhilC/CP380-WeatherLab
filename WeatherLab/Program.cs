using System;
using System.Linq;
using System.Collections.Generic;


namespace WeatherLab
{
    class Program
    {
        static string dbfile = @".\data\climate.db";

        static void Main(string[] args)
        {
            var measurements = new WeatherSqliteContext(dbfile).Weather;

            var total_2020_precipitation = measurements.Where(s => s.year > 2019)
                              .Where(st => st.precipitation >0.0)
                              .Select(s =>s.precipitation);
            double total = 0.0;
            foreach (var name in total_2020_precipitation)
            {
                total = total + name;
            }
            
            Console.WriteLine($"Total precipitation in 2020: {total} mm\n");

            IQueryable<Weather> hdd = (IQueryable<Weather>)measurements.Where(s => s.year > 2015)
                              .Where(st => st.meantemp < 18)
                              .Select(s => new Weather { dateh = (s.year).ToString(), hddv= (s.meantemp - ((s.mintemp + s.maxtemp) / 2)) } );
            
            IQueryable<Weather> cdd = (IQueryable<Weather>)measurements.Where(s => s.year > 2015)
                              .Where(st => st.meantemp >= 18)
                              .Select(s => new Weather { datec = (s.year).ToString(), cvvv = (((s.mintemp + s.maxtemp) / 2) - s.meantemp) });
            //Date = s.meantemp + s.year, (s.meantemp - ((s.mintemp + s.maxtemp) / 2))
            double c16=0, c17=0, c18=0,  c19=0, c20 = 0;
            foreach (var h in hdd)
            {
                if(h.dateh=="2016")
                {
                    c16 = c16 + h.hddv; 
                }
                else if(h.dateh == "2017")
                {
                    c17 = c17 + h.hddv;
                }
                else if (h.dateh == "2018")
                {
                    c18 = c18 + h.hddv;
                }
                else if (h.dateh == "2019")
                {
                    c19 = c19 + h.hddv;
                }
                else
                {
                    c20 = c20 + h.hddv;
                }
            }
            double[] hddarr = { c16, c17, c18, c19, c20 };
            double d16 = 0, d17 = 0, d18 = 0, d19 = 0, d20 = 0;
            foreach (var h in cdd)
            {
                if (h.datec == "2016")
                {
                    d16 = d16 + h.cvvv;
                }
                else if (h.datec == "2017")
                {
                    d17 = d17 + h.cvvv;
                }
                else if (h.datec == "2018")
                {
                    d18 = d18 + h.cvvv;
                }
                else if (h.datec == "2019")
                {
                    d19 = d19 + h.cvvv;
                }
                else
                {
                    d20 = d20 + h.cvvv;
                }
            }
            double[] cddarr = { d16, d17, d18, d19, d20 };
             /* var test1 = measurements.Where(s => s.year > 2015)
                              .Where(st => st.meantemp >= 18)
                              .Select(s => s.meantemp + "-----" + s.year + "==" + (((s.mintemp + s.maxtemp) / 2)-s.meantemp));
            */


            // Heating Degree days have a mean temp of < 18C
            //   see: https://en.wikipedia.org/wiki/Heating_degree_day
            //

            // ?? TODO ??

            //
            // Cooling degree days have a mean temp of >=18C
            //

            // ?? TODO ??

            //
            // Most Variable days are the days with the biggest temperature
            // range. That is, the largest difference between the maximum and
            // minimum temperature
            //
            // Oh: and number formatting to zero pad.
            // 
            // For example, if you want:
            //      var x = 2;
            // To display as "0002" then:
            //      $"{x:d4}"
            //
            Console.WriteLine("Year\tHDD\t\t\tCDD");
            for(int a=0, year=2016;a<5; a++,year++)
            {
                Console.WriteLine(year+"\t"+hddarr[a] + "\t" + cddarr[a]);
            }
          
            Console.WriteLine("---------------------------------------");

            var test11 = measurements.Where(s => s.year > 2015)
                              .OrderByDescending(s => (s.maxtemp - s.mintemp))
                              .Select(s => s.year + "-" + s.month + "-" + s.day + "\t" + (s.maxtemp - s.mintemp));

            Console.WriteLine("\nTop 5 Most Variable Days");
            Console.WriteLine("YYYY-MM-DD\tDelta");
            int m = 0;
            foreach (var fd in test11)
            {
                Console.WriteLine(fd);
                m++;
                if(m>4)
                {
                    break;
                }
            }
            // ?? TODO ??
        }
    }
}
