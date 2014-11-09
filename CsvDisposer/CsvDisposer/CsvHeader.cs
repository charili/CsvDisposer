namespace CsvDisposer
{
    public class CsvHeader
    {
        private string[] _headers;

        public CsvHeader(string[]headers)
        {
            _headers = headers;

        }

        public int GetHeaderIndex(string headerName)
        {
            for (int i = 0; i < _headers.Length; i++)
            {
                if (_headers[i] == headerName)
                    return i;
            }
            return -1;

        }

        public string this[int index]
        {
            get { return _headers[index]; }
        }
        public string[] Values{get { return _headers; }}

        public int Length { get { return _headers.Length; } }

    }
}
