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
        public CsvReader(Stream stream,char splitedCharacter)
        {
            _stream = stream;
            _streamReader = new StreamReader(stream, Encoding.Default);
            _splitedCharacter = splitedCharacter;
            InitCsvHeader();
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
            var currentRow = _streamReader.ReadLine();
            if (currentRow == null)
                return null;
            var splitedRows = currentRow.Split(_splitedCharacter);
            IRow row = new CsvRow(_csvHeader,splitedRows);
            return row;
        }

        public bool HasNextRow()
        {
            int index = _streamReader.Peek();
            return (index != -1);
        }
        public void Dispose()
        {
            if(_streamReader != null)
                _streamReader.Close();
            if(_stream != null)
                _stream.Close();
        }
        
    }
}
