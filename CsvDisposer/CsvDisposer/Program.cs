using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDisposer
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream st = new FileStream(@"f:/abc.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            List<string> a = new List<string>();
            a.Add("abc");
            a.Add("def");
            a.Add("hij");
            using (CsvWriter w = new CsvWriter(st, '\t'))
                w.WriteRow(a);
           



        }
    }
}
