using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvDisposer;

namespace WebserviceTest
{
    public class CsvRow : IRow
    {
        private string[] _cloumnValues;
        private CsvHeader _header;
        private int _columnLength;
        private Dictionary<string, int> _columnNameIndexDic;

        public CsvRow(CsvHeader header,string[] values)
        {
            _header = header;
            _cloumnValues = values;
            _columnLength = values.Length;
        }
        public string GetColumn(string headerName)
        {
            if(string.IsNullOrEmpty(headerName))
                throw new Exception("column name is null");
            if (_header == null)
                throw new Exception("header is null");
            int index = _header.GetHeaderIndex(headerName);
            if(index < 0)
                throw new Exception("invalid header name:" + headerName);
            return _cloumnValues[index];
        }

        public string GetColumn(int columnIndex)
        {
            if(columnIndex >= _columnLength || columnIndex < 0)
                throw new ArgumentOutOfRangeException("columnIndex",columnIndex.ToString());
            return _cloumnValues[columnIndex];
        }

        string IRow.this[int columnIndex]
        {
            get { return GetColumn(columnIndex); }
            set { _cloumnValues[columnIndex] = value; }
        }

        public string this[string columnName]
        {
            get { return GetColumn(columnName); }
        }
    }
}
