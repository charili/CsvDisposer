namespace CsvDisposer
{
    public interface IRow
    {
        string GetColumn(string columnName);
        string GetColumn(int columnIndex);
       
        string this[int columnIndex] { get; set; }
        string this[string columnName] { get;}
    }
}