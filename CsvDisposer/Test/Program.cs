using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //IndexerTest t = new IndexerTest(20);
            //t[2] = 3;
            //Console.WriteLine(t[2]);
            FileStream st = new FileStream(@"f:\text.doc",FileMode.OpenOrCreate,FileAccess.ReadWrite);
            string data = "abcdefghijklmnopqrstuvwxyz";
            byte[] result = new UTF8Encoding(true).GetBytes(data);
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 100000000; i++)
            {
                st.Write(result, 0, result.Length);
                
            }
            st.Close();
            watch.Stop();
            Console.WriteLine("time:"+watch.ElapsedMilliseconds);
            Console.WriteLine("over");

        }
    }
}
