using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDisposer;
using CsvHelper.Configuration;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //1618ms 888ms
            //var converter = TypeDescriptor.GetConverter(prop.PropertyType);
            // var value = converter.ConvertFromString(data[prop.Name]);
            FileStream stream = new FileStream(@"f:/Test1.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //var csvReader = new CsvReader(stream, ',', false);
            TextReader tr = new StreamReader(stream);

            //using (var csvReader = new CsvReader(stream, ',', false))
            //{
            //    csvReader.SetObjectPropertyMapping<Student>();
            //    Stopwatch sw = new Stopwatch();
            //    sw.Start();
            //    for (int i = 0; i < 201132; i++)
            //    {
            //        var students = csvReader.GetRowObject<Student>();
            //    }
            //    sw.Stop();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
                
            //}
            using (var csvHelper = new CsvHelper.CsvReader(tr, new CsvConfiguration { HasHeaderRecord = false }))
            {

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (csvHelper.Read())
                {
                    var st = csvHelper.GetRecord<Student>();

                }

                //var students = csvReader.GetRows<Student>();
                //foreach (var st in students)
                //{
                //    var s = st;

                //}
                //for (int i = 0; i < 201132; i++)
                //{
                //    var students = csvReader.GetRowObject<Student>();
                //}
                //var students = csvReader.GetRows<Student>();
                //foreach (var student in students)
                //{
                //    var s = student;
                //}

                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);


            }

           














            //Console.WriteLine(st.Name);
            //Console.WriteLine(st.Age);
            //Console.WriteLine(st.School);


            //Student st2 = new Student();
            //st2.Name = "liqing";
            //st2.Age = 20;
            //st2.School = "ustc";
            //Console.WriteLine(st2.Name);
            //Console.WriteLine(st2.Age);
            //Console.WriteLine(st2.School);

        }
  
    }
}
