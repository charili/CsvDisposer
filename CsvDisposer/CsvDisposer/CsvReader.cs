using System;
using System.IO;
using System.Text;
using WebserviceTest;

namespace CsvDisposer
{
    public class CsvReader : IDisposable
    {
        private Stream _stream;
        private StreamReader _streamReader;
        private String _csvString;
        private CsvHeader _csvHeader;
        private char _splitedCharacter { get; set; }
        private bool _disposed = false;
        private volatile bool _hasHeader = true;

        public CsvReader(Stream stream, char splitedCharacter, bool hasHeader = true)
        {
            _stream = stream;
            _streamReader = new StreamReader(stream, Encoding.Default);
            _splitedCharacter = splitedCharacter;
            _hasHeader = hasHeader;
            if (_hasHeader)
            {
                InitCsvHeader();
            }
        }

        ~CsvReader()
        {
            Dispose(false);
        }

        public void InitCsvHeader()
        {
            var headerString = _streamReader.ReadLine();
            if (headerString == null)
                return;
            var headers = headerString.Split(_splitedCharacter);
            _csvHeader = new CsvHeader(headers);
            
        }
        public CsvHeader Header{get { return _csvHeader; }}

        public IRow ReadNextRow()
        {
            var currentLine = ReadLine();
            if (currentLine == null)
                return null;
            var splitedLine = currentLine.Split(_splitedCharacter);
            IRow row = new CsvRow(_csvHeader,splitedLine);
            return row;
        }

        private string ReadLine()
        {
            var currentLine = _streamReader.ReadLine();
            return currentLine;
        }

        public T GetRowObject<T>()
        {
            Type type = typeof (T);
            var o = (T)Activator.CreateInstance(type);

            var properties = type.GetProperties();
            var currentLine = ReadLine();
            if (currentLine == null)
                return o;
            var splitedLine = currentLine.Split(_splitedCharacter);
            int i = 0;
            //var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            foreach (var property in properties)
            {
                
                property.SetValue(o, Convert.ChangeType(splitedLine[i],property.PropertyType));
                i++;

            }
            return o;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
                _csvHeader = null;

            }

            if (_streamReader != null)
            {
                _streamReader.Close();
            }


            if (_stream != null)
            {
                _stream.Close();
            }
            _disposed = true;
            _streamReader = null;
            _stream = null;

        }
        
    }
}
