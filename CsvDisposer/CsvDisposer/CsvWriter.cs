using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvDisposer
{
    public class CsvWriter : IDisposable
    {
     
        private IRow _row;
        private Stream _stream;
        private StreamWriter _streamWriter;
        private char _delemeter;
       
        public CsvWriter(Stream stream,char delemeter)
        {
           
            _stream = stream;
            _streamWriter = new StreamWriter(_stream);
            _delemeter = delemeter;
        }


        public void WriteRow(List<string> row)
        {
            if(row == null)
                throw new ArgumentNullException("row");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < row.Count; i++)
            {
                builder.Append(row[i]);
                builder.Append(_delemeter);
                Console.WriteLine(builder);
            }
            builder.Remove(builder.Length - 1, 1);
            _streamWriter.WriteLine(builder);
        }

        public void Dispose()
        {

            if(_streamWriter != null)
                _streamWriter.Close();
            if(_stream != null)
                _stream.Close();
        }
    }
}
