using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace CsvDisposer
{
    public class CsvWriter : IDisposable
    {

        private IRow _row;
        private Stream _stream;
        private StreamWriter _streamWriter;
        private char _delemeter;
        private bool _disposed;
        private bool _fileCompressed;
        private ZipOutputStream _zipOutputStream;
        private CsvHeader _csvHeader;
        private string _headerString;

        public CsvWriter(Stream stream, char delemeter, CsvHeader csvHeader = null,bool fileCompressed = false)
        {
            if(stream == null)
                throw new ArgumentNullException("stream");

            if(fileCompressed)
                _zipOutputStream = new ZipOutputStream(stream);
            else
                _streamWriter = new StreamWriter(stream, Encoding.Default);
            
            _stream = stream;
            _csvHeader = csvHeader;
            _fileCompressed = fileCompressed;
            _delemeter = delemeter;
            
           
        }

        ~CsvWriter()
        {
            Dispose(false);
        }

        private void GenHeaderString()
        {
            
            if (_csvHeader != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < _csvHeader.Length; i ++)
                {
                    if (i != sb.Length - 1)
                    {
                        sb.Append(_csvHeader.Values[i]);
                        sb.Append(_delemeter);
                    }
                    else
                    {
                        sb.AppendLine(_csvHeader.Values[i]);
                    }
                }
                _headerString = sb.ToString();
            }
            
        }
        public void WriteRow(List<string> rows)
        {
            if (rows == null)
                throw new ArgumentNullException("rows");
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < rows.Count; i++)
            {
                builder.Append(rows[i]);
                if(i!=rows.Count-1)
                    builder.Append(_delemeter);
                
            }
           
            _streamWriter.WriteLine(builder);
            
        }

        public async Task WriteRowsAsync<T>(List<T> records)
        {
            var type = typeof (T);
            var properties = type.GetProperties();
            StringBuilder builder = new StringBuilder();
            if (_csvHeader != null)
            {
                GenHeaderString();
                builder.AppendLine(_headerString);
            }
           
            foreach (var record in records)
            {
                for (var i = 0; i < properties.Length; i++)
                {
                    builder.Append(properties[i].GetValue(record));
                    if(i == properties.Length - 1)
                        break;
                    builder.Append(_delemeter);
                }

                await _streamWriter.WriteLineAsync(builder.ToString());
                builder.Clear();
            }
            await _streamWriter.FlushAsync();
        }


        private List<string> GenMultiCsvFileString<T>(List<T> records,int recordNum)
        {
            if (records == null)
                throw new ArgumentNullException("records");

            StringBuilder builder = new StringBuilder();
            List<string> results = new List<string>();
            GenHeaderString();
            var type = typeof(T);
            var properties = type.GetProperties();

            for (var i = 0; i < records.Count; i++)
            {
                if (i == 0)
                {
                    builder.AppendLine(_headerString);
                }
                else if (i % recordNum == 0)
                {
                    results.Add(builder.ToString());
                    builder.Clear();
                    builder.AppendLine(_headerString);
                }
                for (var j = 0; j < properties.Length; j++)
                {
                    
                    if (j == properties.Length - 1)
                        builder.AppendLine(properties[j].GetValue(records[i]).ToString());
                    else
                    {
                        builder.Append(properties[j].GetValue(records[i]));
                        builder.Append(_delemeter);
                    }
                    
                }
            }
            results.Add(builder.ToString());
            builder.Clear();
            return results;
        }


        public async  Task WriteCompressedFileAsync<T>(string compressFileName, string prefixFileName, List<T> records, int recordNum)
        {
            var content = GenMultiCsvFileString(records, recordNum);
            int count = 0;
            
            _zipOutputStream.AlternateEncoding = Encoding.Default;
            foreach (string str in content)
            {
                _zipOutputStream.PutNextEntry(String.Format(prefixFileName + "{0}.csv", count++));
                byte[] buffer = Encoding.Default.GetBytes(str);
                await _zipOutputStream.WriteAsync(buffer, 0, buffer.Length);
                await _zipOutputStream.FlushAsync();
            }
        }


        public void Flush()
        {
            _streamWriter.Flush();
        }

        public async void FlushAsync()
        {
            await _streamWriter.FlushAsync();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            
            if (_streamWriter != null)
                _streamWriter.Close();
            if(_fileCompressed && _zipOutputStream != null)
                _zipOutputStream.Close();
            if (_stream != null)
                _stream.Close();

            _disposed = true;
            _streamWriter = null;
            _zipOutputStream = null;
            _stream = null;
        }

    }
}
